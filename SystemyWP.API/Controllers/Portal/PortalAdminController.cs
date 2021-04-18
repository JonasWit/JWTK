using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class PortalAdminController : ApiController
    {
        public PortalAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("clients")]
        public async Task<IActionResult> ListClients(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var users = await userManager.GetUsersForClaimAsync(
                SystemyWPConstants.Claims.ClientClaim);

            return Ok(users.Select(x => new
            {
                x.Id,
                x.Email
            }));
        }

        [HttpPost("clients")]
        public async Task<IActionResult> InviteClient(
            [FromBody] InviteClientForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] EmailClient emailClient)
        {
            var existingUser = await userManager.FindByEmailAsync(form.Email);
            if (existingUser is not null) return BadRequest("User with this email already exists");

            var client = new IdentityUser
            {
                UserName = form.Email,
                Email = form.Email
            };

            var randomPart = new Random().Next(1000000000, int.MaxValue);
            var createResult = await userManager.CreateAsync(client, $"{randomPart}a1!A");

            if (!createResult.Succeeded)
            {
                var errorResponse = createResult.Errors
                    .Aggregate("Failed to create user:", (a, b) => $"{a} {b.Description}");

                return BadRequest(errorResponse);
            }

            await userManager.AddClaimAsync(client, SystemyWPConstants.Claims.InvitedClaim);
            var code = await userManager.GeneratePasswordResetTokenAsync(client);

            var link = Url.Page("/Account/Client", "Get", new
            {
                email = form.Email,
                returnUrl = form.ReturnUrl,
                code
            }, HttpContext.Request.Scheme);

            await emailClient.SendClientInvite(form.Email, link);

            return Ok(link);
        }
    }
}