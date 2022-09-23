using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Data;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Data.DTOs.General.UserForms;
using SystemyWP.API.Data.Models;
using SystemyWP.API.Data.Repositories;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Services.JWTServices;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Controllers.MasterService
{
    [Route("[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly EmailClient _emailClient;
        private readonly GastronomyHttpClient _gastronomyHttpClient;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IOptionsMonitor<CorsSettings> _corsSettings;
        private readonly Encryptor _encryptor;
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(
            EmailClient emailClient,
            GastronomyHttpClient gastronomyHttpClient,
            ILogger<AuthController> logger,
            AppDbContext context,
            IOptionsMonitor<CorsSettings> corsSettings,
            Encryptor encryptor,
            UserRepository userRepository,
            TokenService tokenService)
        {
            _emailClient = emailClient;
            _gastronomyHttpClient = gastronomyHttpClient;
            _logger = logger;
            _context = context;
            _corsSettings = corsSettings;
            _encryptor = encryptor;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("authorized-check")]
        public IActionResult AuthorizedCheck() => Ok(new { Message = "Authorized Response", UserEmail, UserId });

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsForm userCredentialsForm)
        {
            try
            {
                if (_userRepository.UserExists(userCredentialsForm.Email))
                {
                    return BadRequest();
                }

                var ecToken = _tokenService.GenerateEmailConfirmationToken(userCredentialsForm.Email);

                var qb = new QueryBuilder { { "token", ecToken } };
                var callbackUrl = $@"{_corsSettings.CurrentValue.ApiUrl}/auth/confirm-email/{qb}";

                Response sgResponse = await _emailClient.SendEmailConfirmationAsync(
                userCredentialsForm.Email,
                callbackUrl);

                if (sgResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception("Confirmation email not sent");
                }

                User newUser = _userRepository.CreateUser(userCredentialsForm);
                if (await _userRepository.SaveChanges() == 0)
                {
                    throw new Exception("User not created");
                }

                _userRepository.UpdateConfirmEmailToken(newUser.Id, ecToken);
                return await _userRepository.SaveChanges() == 0 ? throw new Exception("Confirmation token not saved") : (IActionResult)Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Registration");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [HttpGet("confirm-email", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            try
            {
                if (!_tokenService.ValidateToken(token))
                {
                    return BadRequest();
                }

                var email = _tokenService.GetTokenClaim(token, ClaimTypes.Email);
                var user = _userRepository.GetUserId(email);

                if (user is null)
                {
                    return Ok();
                }

                _userRepository.UpdateConfirmEmailToken(user, token);

                if (await _userRepository.SaveChanges() > 0)
                {
                    return Redirect($@"{_corsSettings.CurrentValue.PortalUrl}/{AppConstants.ClientRoutes.EmailConfirmed}");
                }

                return Redirect($@"{_corsSettings.CurrentValue.PortalUrl}/{AppConstants.ClientRoutes.EmailNotConfirmed}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Password Reset Request");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [HttpPost("authenticate", Name = "Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentialsForm userCredentialsForm)
        {
            try
            {
                userCredentialsForm.Password = _encryptor.Encrypt(userCredentialsForm.Password);
                User loggedInUser = _userRepository
                    .GetUser(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == userCredentialsForm.Email) &&
                                u.Password == userCredentialsForm.Password);

                if (loggedInUser is null)
                {
                    return NotFound();
                }

                if (!loggedInUser.EmailConfirmed)
                {
                    return NotFound();
                }

                loggedInUser.LastLogin = DateTime.UtcNow;
                _ = _context.Update(loggedInUser);
                _ = await _context.SaveChangesAsync();

                var token = _tokenService.GenerateJwtToken(loggedInUser);
                return Ok(new TokenDto() { Token = token });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [Authorize]
        [HttpDelete("delete-account", Name = "DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromServices] IWebHostEnvironment env)
        {
            try
            {
                var key = _userRepository.GetUserAccessKey(UserId);

                HttpStatusCode gastroCleanUp = await _gastronomyHttpClient.RemoveAllData(key);

                if (env.IsDevelopment())
                {
                    return NoContent();
                }

                if (gastroCleanUp == HttpStatusCode.OK)
                {
                    _userRepository.DeleteAccount(UserId);
                    if (await _userRepository.SaveChanges() > 0)
                    {
                        return NoContent();
                    }
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [HttpPost("reset-password-request", Name = "ResetPasswordRequest")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] UserEmailForm userEmailForm)
        {
            try
            {
                User user = _userRepository.GetUser(user => user.Claims.Any(claim =>
                    claim.ClaimType == ClaimTypes.Email && claim.ClaimValue == userEmailForm.Email));

                if (user is null)
                {
                    return Ok();
                }

                var resetToken = _tokenService.GeneratePasswordResetJwtToken(user);

                var qb = new QueryBuilder { { "token", resetToken } };
                var callbackUrl = $@"{_corsSettings.CurrentValue.PortalUrl}/reset-password/{qb}";

                Response sgResponse = await _emailClient.SendPasswordResetAsync(
                        user.Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Email)?.ClaimValue,
                        callbackUrl);

                if (sgResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception("Reset email not sent");
                }

                _userRepository.UpdateResetPasswordToken(user.Id, resetToken);
                return await _userRepository.SaveChanges() == 0 ? throw new Exception("Reset token not saved") : (IActionResult)Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Password Reset Request");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [HttpPost("reset-password", Name = "ResetPasswordAction")]
        public async Task<IActionResult> ResetPasswordAction([FromBody] UserPasswordResetForm userPasswordResetForm)
        {
            try
            {
                if (!_tokenService.ValidateToken(userPasswordResetForm.Token))
                {
                    return BadRequest();
                }

                var email = _tokenService.GetTokenClaim(userPasswordResetForm.Token, ClaimTypes.Email);
                User user = _userRepository
                    .GetUser(user => user.Claims.Any(claim => claim.ClaimType.Equals(AppConstants.ClaimNames.PasswordResetToken) && claim.ClaimType.Equals(userPasswordResetForm.Token)) &&
                        user.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == email));

                if (user is null)
                {
                    return BadRequest();
                }

                var password = _encryptor.Encrypt(userPasswordResetForm.Password);
                _userRepository.ChangePassword(user.Id, password);

                if (await _userRepository.SaveChanges() > 0)
                {
                    return Redirect($@"{_corsSettings.CurrentValue.PortalUrl}/{AppConstants.ClientRoutes.PasswordReset}");
                }
                return Redirect($@"{_corsSettings.CurrentValue.PortalUrl}/{AppConstants.ClientRoutes.PasswordNotReset}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Password Reset Action");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }

        [Authorize]
        [HttpPost("change-password", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordForm changePasswordForm)
        {
            try
            {
                var newPassword = _encryptor.Encrypt(changePasswordForm.NewPassword);
                var oldPassword = _encryptor.Encrypt(changePasswordForm.OldPassword);
                User loggedInUser = _userRepository
                    .GetUser(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue == UserEmail) &&
                                  u.Password == oldPassword);
                if (loggedInUser is null)
                {
                    return NotFound();
                }

                _userRepository.ChangePassword(UserId, newPassword);
                return await _userRepository.SaveChanges() > 0 ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
            }
        }
    }
}