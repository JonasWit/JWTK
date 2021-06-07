using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.Services.Storage;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API.Controllers.Access
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
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env)
        {
            await signInManager.SignOutAsync();
            return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] IFileProvider fileManager)
        {
            var uId = UserId;
            try
            {
                var user = await userManager.FindByIdAsync(uId);

                await userManager.UpdateSecurityStampAsync(user);
                await signInManager.SignOutAsync();

                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(uId));
                if (!string.IsNullOrEmpty(userProfile?.Image))
                    await fileManager.DeleteProfileImageAsync(userProfile.Image);

                if (userProfile is not null) _context.Remove(userProfile);
                await _context.SaveChangesAsync();

                await userManager.DeleteAsync(user);

                return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, uId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}