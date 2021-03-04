using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Email;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SystemyWP.API.Controllers
{
    [Route("/api/admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class PortalAdminController : ApiController
    {
        private readonly AppDbContext _context;
        private readonly ApiIdentityDbContext _apiIdentityContext;

        public PortalAdminController(AppDbContext context, 
            ApiIdentityDbContext apiIdentityContext)
        {
            _context = context;
            _apiIdentityContext = apiIdentityContext;
        }
        
        [HttpGet("clients")]
        public async Task<IActionResult> ListAccessKeys(
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

        [HttpGet("access-keys")]
        public async Task<IActionResult> ListClients(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var dataAccessKeys = _apiIdentityContext.UserClaims
                .Where(x => x.ClaimType.Equals(SystemyWPConstants.Claims.DataAccessKey))
                .Select(x => x.ClaimValue).Distinct().ToList();

            return Ok(dataAccessKeys);
        }

        [HttpPost("client/grantAccessKey")]
        public async Task<IActionResult> GrantDataAccessKey(
            [FromBody] GrantDataAccessKey form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);

            if (user is null)
            {
                return BadRequest("There is no user with this ID!");
            }

            var userClaims = await userManager.GetClaimsAsync(user) as List<Claim>;

            if (userClaims.Any(x => x.Type.Equals(SystemyWPConstants.Claims.DataAccessKey)))
            {
                await userManager
                    .RemoveClaimsAsync(user,
                        userClaims
                            .Where(x => x.Type.Equals(SystemyWPConstants.Claims.DataAccessKey))
                            .ToList());
            }

            var result = await userManager
                .AddClaimAsync(user, new Claim(SystemyWPConstants.Claims.DataAccessKey, form.DataAccessKey));

            if (result.Succeeded)
            {
                return Ok($"Data Access Key {form.DataAccessKey} Added!");
            }
            else
            {
                return BadRequest("Error when adding the Claim!");
            }
        }

        [HttpPost("client/revokeAccessKey")]
        public async Task<IActionResult> RevokeDataAccessKey(
            [FromBody] RevokeDataAccessKey form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);

            if (user is null)
            {
                return BadRequest("There is no user with this ID!");
            }

            var userClaims = await userManager.GetClaimsAsync(user) as List<Claim>;

            if (userClaims.Any(x => x.Type.Equals(SystemyWPConstants.Claims.DataAccessKey)))
            {
                var result = await userManager
                    .RemoveClaimsAsync(user,
                        userClaims
                            .Where(x => x.Type.Equals(SystemyWPConstants.Claims.DataAccessKey))
                            .ToList());
                if (result.Succeeded)
                {
                    return Ok($"Data Access Key removed!");
                }
                else
                {
                    return BadRequest("Error during Claim removal!");
                }
            }
            else
            {
                return BadRequest("Error user has no Data Access Key Claim!");
            }
        }

        [HttpGet("users")]
        public async Task<List<UserProjections.UserViewModel>> ListUsers(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] AppDbContext context)
        {
            var userIds = userManager.Users
                .Select(x => x.Id)
                .ToList();

            var identityUsers = userManager.Users.ToList();
            var result = new List<UserProjections.UserViewModel>();

            foreach (var identityUser in identityUsers)
            {
                var userClaims = await userManager.GetClaimsAsync(identityUser) as List<Claim>;
                result.Add(new UserProjections.UserViewModel
                {
                    Id = identityUser.Id,
                    Username = identityUser.UserName,
                    Email = identityUser.Email,
                    AllowedApps = userClaims
                        .Where(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess))
                        .Select(x => x.Value).ToList(),
                    Image = context.Users
                        .FirstOrDefault(x => x.Id.Equals(identityUser.Id)).Image,
                    DataAccessKey = userClaims
                        .FirstOrDefault(x =>
                            x.Type.Equals(SystemyWPConstants.Claims.DataAccessKey))?.Value,
                    Role = userClaims
                        .FirstOrDefault(x =>
                            x.Type.Equals(SystemyWPConstants.Claims.Role))?.Value,
                    Locked = identityUser.LockoutEnd is not null,
                    EmailConfirmed = identityUser.EmailConfirmed
                });
            }

            return result;
        }

        [HttpPost("user/lock")]
        public async Task<IActionResult> LockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            var user = await userManager.FindByIdAsync(form.UserId);

            if (user is null)
            {
                return BadRequest("There is no user with this ID!");
            }
            
            var result = await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(+25));

            if (result.Succeeded) return Ok("User locked!");
            return BadRequest("Error during user lock!");
        }
        
        [HttpPost("user/unlock")]
        public async Task<IActionResult> UnlockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            var user = await userManager.FindByIdAsync(form.UserId);

            if (user is null)
            {
                return BadRequest("There is no user with this ID!");
            }
            
            var result = await userManager.SetLockoutEndDateAsync(user, null);

            if (result.Succeeded) return Ok("User locked!");
            return BadRequest("Error during user lock!");
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

            await userManager.AddClaimAsync(client, SystemyWPConstants.Claims.ClientClaim);
            var code = await userManager.GeneratePasswordResetTokenAsync(client);

            var link = Url.Page("/Account/Client", "Get", new
            {
                email = form.Email,
                returnUrl = form.ReturnUrl,
                code,
            }, protocol: HttpContext.Request.Scheme);

            await emailClient.SendClientInvite(form.Email, link);

            return Ok(link);
        }

        [HttpPost("user/role")]
        public async Task<IActionResult> ChangeRole(
            [FromBody] RolesManagementForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {



            return Ok();
        }
        
    }
}