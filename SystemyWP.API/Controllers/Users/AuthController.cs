using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SendGrid.Helpers.Errors.Model;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Controllers.Users
{
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ApiController
    {
        public AuthController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] PortalLogger portalLogger,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env)
        {
            var user = await userManager.FindByIdAsync(UserId);
            await signInManager.SignOutAsync();
            await portalLogger.Log(HttpContext.Request.Path.Value, user, LogType.Access, "Wylogowano",
                "Logout Endpoint");
            return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "https://portal.systemywp.pl/");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] UserLoginForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] PortalLogger portalLogger,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IHostEnvironment env)
        {
            try
            {
                var returnUrl = env.IsDevelopment() ? @"https://localhost:3000/" : @"https://portal.systemywp.pl";
                var signInResult = await signInManager
                    .PasswordSignInAsync(form.Username, form.Password, true, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(form.Username);
                    await portalLogger.Log(HttpContext.Request.Path.Value, user, LogType.Access, "Logged in",
                        "Auth endpoint");
                    return Redirect(returnUrl);
                }

                if (signInResult.IsLockedOut)
                {
                    var user = await userManager.FindByNameAsync(form.Username);
                    await portalLogger.Log(HttpContext.Request.Path.Value, user, LogType.Access, "Failed login attempt - account locked", "Auth endpoint");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Account locked");
                }

                if (!signInResult.Succeeded)
                {
                    await portalLogger
                        .Log(new PortalLogRecord
                        {
                            Endpoint = HttpContext.Request.Path.Value,
                            LogType = LogType.Access,
                            Description = "Failed login attempt",
                            CreatedBy = "Auth endpoint",
                            UserEmail = "",
                            UserName = form.Username,
                            UserId = "",
                        });
                }
                return NotFound();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] IFileProvider fileManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(UserId);
                if (user is null) return BadRequest("User not found");

                await userManager.UpdateSecurityStampAsync(user);
                await signInManager.SignOutAsync();

                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
                //if (!string.IsNullOrEmpty(userProfile?.Image)) await fileManager.DeleteFileAsync(userProfile.Image);

                if (userProfile is not null) _context.Remove(userProfile);
                await _context.SaveChangesAsync();

                var deleteResult = await userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
                }

                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}