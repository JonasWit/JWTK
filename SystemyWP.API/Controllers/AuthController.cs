using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiController
    {

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(string logoutId, 
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env,
            [FromServices] PortalLogger logger)
        {
            await signInManager.SignOutAsync();
            return Redirect(env.IsDevelopment() ? "https://localhost:3000/" : "/");
        }

        public AuthController(PortalLogger portalLogger) : base(portalLogger)
        {
        }
    }
}