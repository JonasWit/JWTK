using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API.Controllers
{
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ApiController
    {

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] PortalLogger logger)
        {
            await signInManager.SignOutAsync();
            return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
        }
        
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] PortalLogger logger,
            [FromServices] AppDbContext context,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                await signInManager.SignOutAsync();
                var user = await userManager.FindByIdAsync(UserId);
                await userManager.UpdateSecurityStampAsync(user);

                var userProfile = context.Users.FirstOrDefault(x => x.Id.Equals(UserId));

                if (userProfile is not null)
                {
                    context.Remove(userProfile);
                }
                
                await context.SaveChangesAsync();
                return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
            }
            catch (Exception e)
            {
                await logger.Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
            }
        }

        public AuthController(PortalLogger portalLogger) : base(portalLogger)
        {
        }
    }
}