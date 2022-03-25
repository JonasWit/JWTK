using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models;
using SystemyWP.API.DTOs;
using SystemyWP.API.Forms;
using SystemyWP.API.Repositories;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.JWTServices;
using SystemyWP.API.Settings;
using SystemyWP.API.Utilities;

namespace SystemyWP.API.Controllers
{
    [Route("[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IOptionsMonitor<AuthSettings> _authSettings;
        private readonly IOptionsMonitor<CorsSettings> _corsSettings;
        private readonly Encryptor _encryptor;
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(
            ILogger<AuthController> logger,
            AppDbContext context,
            IOptionsMonitor<AuthSettings> authSettings,
            IOptionsMonitor<CorsSettings> corsSettings,
            Encryptor encryptor,
            IUserRepository userRepository,
            TokenService tokenService)
        {
            _logger = logger;
            _context = context;
            _authSettings = authSettings;
            _corsSettings = corsSettings;
            _encryptor = encryptor;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("authorized-check")]
        public IActionResult AuthorizedCheck() => Ok(new {Message= "Authorized Response", UserEmail, UserId });

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsForm userCredentialsForm)
        {
            try
            {
                var emailAddressExists = _userRepository.GetUser(u =>
                    u.Claims.Any(uc => uc.ClaimType == ClaimTypes.Email && uc.ClaimValue == userCredentialsForm.Email));
                if (emailAddressExists is not null) return BadRequest();
                
                _userRepository.CreateUser(userCredentialsForm);
                if (await _userRepository.SaveChanges() > 0) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
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
                
                var loggedInUser = _userRepository
                    .GetUser(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == userCredentialsForm.Email) &&
                                u.Password == userCredentialsForm.Password);
                if (loggedInUser is null) return NotFound();
                
                loggedInUser.LastLogin = DateTime.UtcNow;
                _context.Update(loggedInUser);
                await _context.SaveChangesAsync();
                
                var token = _tokenService.GenerateJwtToken(loggedInUser);
                return Ok(new TokenDto() {Token = token});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return ServerError;
            }
        }
        
        [Authorize]
        [HttpPost("delete-account", Name = "DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                _userRepository.DeleteAccount(UserId);
                if (await _userRepository.SaveChanges() > 0) return NoContent();
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return ServerError;
            }
        }
        
        [HttpPost("reset-password-request", Name = "ResetPasswordRequest")]
        public async Task<IActionResult> ResetPasswordRequest(
            [FromBody] UserEmailForm userEmailForm,
            [FromServices] EmailClient emailClient)
        {
            try
            {
                var user = _userRepository.GetUser(user => user.Claims.Any(claim =>
                    claim.ClaimType == ClaimTypes.Email && claim.ClaimValue == userEmailForm.Email));

                if (user is null)  return Ok();
                var resetToken = _tokenService.GeneratePasswordResetJwtToken(user);

                _userRepository.UpdateResetPasswordTokenToken(user.Id, resetToken);

                var qb = new QueryBuilder {{"target", "password-reset"}, {"token", resetToken}};
                var callbackUrl = $@"{_corsSettings.CurrentValue.PortalUrl}/auth/reset-password/{qb}";
                
                await emailClient.SendEmailAsync(
                    user.Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Email)?.ClaimValue,
                    "Reset Hasła",
                    EmailTemplates.PasswordResetButtonEmailBody(callbackUrl));
                
                if (await _userRepository.SaveChanges() > 0) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Password Reset Request");
                return ServerError;
            }
        }
        
        [HttpPost("reset-password-action", Name = "ResetPasswordAction")]
        public async Task<IActionResult> ResetPasswordAction([FromBody] UserPasswordResetForm userPasswordResetForm)
        {
            try
            {
                if (!_tokenService.ValidateToken(userPasswordResetForm.Token)) return BadRequest();
                var email = _tokenService.GetTokenClaim(userPasswordResetForm.Token, ClaimTypes.Email);
                
                var user = _userRepository
                    .GetUser(user => user.PasswordResetToken == userPasswordResetForm.Token && user.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == email));

                if (user is null) return BadRequest();
                
                var password = _encryptor.Encrypt(userPasswordResetForm.Password);
                _userRepository.ChangePassword(user.Id, password);
                if (await _userRepository.SaveChanges() > 0) return Ok();
                
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Password Reset Action");
                return ServerError;
            }
        }
        
        [Authorize]
        [HttpPost("change-password", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordForm changePasswordForm)
        {
            try
            {
                var newPassword = _encryptor.Encrypt(changePasswordForm.NewPassword);
                var oldPassword = _encryptor.Encrypt(changePasswordForm.OldPassword);
                
                var loggedInUser = _userRepository
                    .GetUser(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == UserEmail) &&
                                  u.Password == oldPassword);
                if (loggedInUser is null) return NotFound();
                
                _userRepository.ChangePassword(UserId, newPassword);
                if (await _userRepository.SaveChanges() > 0) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return ServerError;
            }
        }
    }
}