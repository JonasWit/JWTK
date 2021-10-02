using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Enums;

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
                if (!string.IsNullOrEmpty(userProfile?.Image)) await fileManager.DeleteFileAsync(userProfile.Image);

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