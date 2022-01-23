using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Data.Models.UsersManagement.Access;
using SystemyWP.API.DTOs;
using SystemyWP.API.Forms.User;
using SystemyWP.API.Repositories.General;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Controllers.Users
{
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IOptionsMonitor<AuthSettings> _optionsMonitor;
        private readonly Encryptor _encryptor;
        private readonly IUserRepository _userRepository;

        public AuthController(
            IHostingEnvironment hostingEnvironment,
            ILogger<AuthController> logger,
            AppDbContext context,
            IOptionsMonitor<AuthSettings> optionsMonitor,
            Encryptor encryptor,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _context = context;
            _optionsMonitor = optionsMonitor;
            _encryptor = encryptor;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet("register")]
        public IActionResult AuthorizedCheck()
        {
            return Ok("Authorized response");
        }

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsForm userCredentialsForm)
        {
            try
            {
                var emailAddressExists =
                    _context.Users
                        .Include(x => x.Claims)
                        .FirstOrDefault(u => u.Claims.Any(uc => uc.ClaimType == ClaimTypes.Email && uc.ClaimValue == userCredentialsForm.Email));
                if (emailAddressExists is not null) return BadRequest();

                var newGuid = Guid.NewGuid().ToString();
                var newUser = new User()
                {
                    Id = newGuid,
                    Password = _encryptor.Encrypt(userCredentialsForm.Password),
                    AccessKey = new AccessKey
                    {
                        Id = Guid.NewGuid().ToString(),
                    },
                    Claims = new List<UserClaim>
                    {
                        new UserClaim
                        {
                            ClaimType = ClaimTypes.Role,
                            ClaimValue = SystemyWpConstants.Roles.User
                        },
                        new UserClaim
                        {
                            ClaimType = ClaimTypes.Email,
                            ClaimValue = userCredentialsForm.Email
                            
                        },
                        new UserClaim
                        {
                            ClaimType = ClaimTypes.Name,
                            ClaimValue = userCredentialsForm.Email
                        },
                        new UserClaim
                        {
                            ClaimType = ClaimTypes.NameIdentifier,
                            ClaimValue = newGuid
                        },
                    }
                };

                _userRepository.CreateUser(newUser);
                await _userRepository.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                if (_hostingEnvironment.IsDevelopment()) Console.WriteLine(SystemyWpConstants.ExceptionConsoleMessage(e));
                _logger.LogError(e, "Issue during Registration");
                return ServerError;
            }
        }

        [HttpPost("authenticate", Name = "Authenticate")]
        public async Task<IActionResult> Authenticate(UserCredentialsForm userCredentialsForm)
        {
            try
            {
                userCredentialsForm.Password = _encryptor.Encrypt(userCredentialsForm.Password);
                var loggedInUser = await _context.Users
                    .Include(u => u.Claims)
                    .Where(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == userCredentialsForm.Email) &&
                                u.Password == userCredentialsForm.Password)
                    .FirstOrDefaultAsync();

                if (loggedInUser is null) return NotFound();

                var token = GenerateJwtToken(loggedInUser);
                return Ok(new TokenDto() {Token = token});
            }
            catch (Exception e)
            {
                if (_hostingEnvironment.IsDevelopment()) Console.WriteLine(SystemyWpConstants.ExceptionConsoleMessage(e));
                _logger.LogError(e, "Issue during Authentication");
                return ServerError;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_optionsMonitor.CurrentValue.SecretKey);

                var claimEmail = new Claim(ClaimTypes.Email, user.Claims.First(c => c.ClaimType == ClaimTypes.Email).ClaimValue);
                var claimName = new Claim(ClaimTypes.Name, user.Claims.First(c => c.ClaimType == ClaimTypes.Name).ClaimValue);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Claims.First(c => c.ClaimType == ClaimTypes.NameIdentifier).ClaimValue);
                var claimRole = new Claim(ClaimTypes.Role, user.Claims.First(c => c.ClaimType == ClaimTypes.Role).ClaimValue);
            
                var claimsIdentity = new ClaimsIdentity(new[] {claimEmail, claimNameIdentifier, claimRole, claimName},
                    SystemyWpConstants.AuthenticationType.ServerAuth);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                };

                //creating a token handler
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                //returning the token back
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue when generating JWT token");
                throw new Exception();
            }

        }
    }
}