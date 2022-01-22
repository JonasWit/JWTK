using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;
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
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IOptionsMonitor<AuthSettings> _optionsMonitor;
        private readonly Encryptor _encryptor;
        private readonly IUserRepository _userRepository;

        public AuthController(
            ILogger<AuthController> logger,
            AppDbContext context,
            IOptionsMonitor<AuthSettings> optionsMonitor,
            Encryptor encryptor,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _optionsMonitor = optionsMonitor;
            _encryptor = encryptor;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsForm userCredentialsForm)
        {
            try
            {
                var emailAddressExists =
                    _context.Users.FirstOrDefault(u => u.EmailAddress == userCredentialsForm.Email);
                if (emailAddressExists is not null) return Forbid();

                var newUser = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Password = _encryptor.Encrypt(userCredentialsForm.Password),
                    EmailAddress = userCredentialsForm.Email,
                    Role = SystemyWpConstants.Roles.User
                };

                _userRepository.CreateUser(newUser);
                await _userRepository.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(SystemyWpConstants.ExceptionConsoleMessage(e));
                return ServerError;
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserCredentialsForm userCredentialsForm)
        {
            try
            {
                userCredentialsForm.Password = _encryptor.Encrypt(userCredentialsForm.Password);
                var loggedInUser = await _context.Users
                    .Where(u => u.EmailAddress == userCredentialsForm.Email &&
                                u.Password == userCredentialsForm.Password)
                    .FirstOrDefaultAsync();

                if (loggedInUser is null) return NotFound();

                var token = GenerateJwtToken(loggedInUser);
                return Ok(new TokenDto() {Token = token});
            }
            catch (Exception e)
            {
                Console.WriteLine(SystemyWpConstants.ExceptionConsoleMessage(e));
                return ServerError;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
            
            var claimEmail = new Claim(ClaimTypes.Email, user.EmailAddress);
            var claimName = new Claim(ClaimTypes.Name, user.EmailAddress);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id);
            var claimRole = new Claim(ClaimTypes.Role, user.Role);
            
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimRole, claimName }, SystemyWpConstants.AuthenticationType.ServerAuth);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //returning the token back
            return tokenHandler.WriteToken(token);
        }
        
        
        
        
        


 
    }
}