using System;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
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
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(UserId);
                await signInManager.SignOutAsync();
                await userManager.UpdateSecurityStampAsync(user);





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