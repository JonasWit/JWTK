using System;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API.Controllers.Access
{
    [Route("api/access")]
    public class ApiAccessController : ApiController
    {
        public ApiAccessController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpPost("admin/dev-gate/login")]
        public async Task<IActionResult> Login(
            [FromBody] UserLoginForm form,
            [FromServices] PortalLogger portalLogger,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices]IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                await portalLogger.Log(LogType.Issue, HttpContext.Request.Path.Value, "-", "-",
                    $"Api endpoint login attempt! - {form.Username}");           
                return NotFound();
            }
            
            try
            {
                await portalLogger.Log(LogType.Access, HttpContext.Request.Path.Value, "-", "-",
                    $"Api login attempt - {form.Username}");

                var signInResult = await signInManager
                    .PasswordSignInAsync(form.Username, form.Password, true, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    return Ok();
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