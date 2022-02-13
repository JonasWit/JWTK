﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.DTOs;
using SystemyWP.API.Forms.User;
using SystemyWP.API.Repositories.General;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Controllers
{
    [Route("[controller]")]
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
            IUserRepository userRepository)
        {
            _logger = logger;
            _context = context;
            _optionsMonitor = optionsMonitor;
            _encryptor = encryptor;
            _userRepository = userRepository;
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
                
                var token = GenerateJwtToken(loggedInUser);
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
        
        [Authorize]
        [HttpPost("change-password", Name = "ResetPassword")]
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
                await _userRepository.SaveChanges();
                
                if (await _userRepository.SaveChanges() > 0) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue during Authentication");
                return ServerError;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Claims.First(c => c.ClaimType == ClaimTypes.Email).ClaimValue),
                    new Claim(ClaimTypes.Name, user.Claims.First(c => c.ClaimType == ClaimTypes.Name).ClaimValue),
                    new Claim(ClaimTypes.Role, user.Claims.First(c => c.ClaimType == ClaimTypes.Role).ClaimValue),
                    new Claim(ClaimTypes.NameIdentifier, user.Claims.First(c => c.ClaimType == ClaimTypes.NameIdentifier).ClaimValue),
                };

                var secretBytes = Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
                var key = new SymmetricSecurityKey(secretBytes);

                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var token = new JwtSecurityToken(
                    _optionsMonitor.CurrentValue.Issuer, 
                    _optionsMonitor.CurrentValue.Audience, 
                    claims,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(7),
                    signingCredentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenJson;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Issue when generating JWT token");
                throw new Exception();
            }
        }
    }
}