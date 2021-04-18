using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-admin/user-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class UserAdminController : ApiController
    {
        public UserAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("users")]
        public async Task<List<UserProjections.UserViewModel>> ListUsers(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var identityUsers = userManager.Users.ToList();

            var result = new List<UserProjections.UserViewModel>();
            foreach (var identityUser in identityUsers)
            {
                var userClaims = await userManager.GetClaimsAsync(identityUser) as List<Claim>
                                 ?? new List<Claim>();

                var accessKey = _context.AccessKeys
                    .Include(x => x.Users)
                    .Where(x => x.Users.Any(x => x.Id.Equals(identityUser.Id)))
                    .Select(AccessKeyProjection.Projection)
                    .FirstOrDefault();

                result.Add(new UserProjections.UserViewModel
                {
                    Id = identityUser.Id,
                    Username = identityUser.UserName,
                    Email = identityUser.Email,
                    LegalAppAllowed = userClaims
                        .Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess)),
                    Image = _context.Users
                        .FirstOrDefault(x => x.Id.Equals(identityUser.Id))?.Image,
                    DataAccessKey = accessKey,
                    Role = userClaims
                        .FirstOrDefault(x =>
                            x.Type.Equals(SystemyWPConstants.Claims.Role))?.Value,
                    Locked = identityUser.LockoutEnd is not null,
                    EmailConfirmed = identityUser.EmailConfirmed
                });
            }

            return result.OrderByDescending(x => x.Email).ToList();
        }

        [HttpPost("user/lock")]
        public async Task<IActionResult> LockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            if (user is null) return BadRequest("There is no user with this ID!");

            var result = await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(+25));
            if (result.Succeeded)
            {
                await userManager.UpdateSecurityStampAsync(user);
                return Ok("User locked!");
            }

            return BadRequest("Error during lock!");
        }

        [HttpPost("user/unlock")]
        public async Task<IActionResult> UnlockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            if (user is null) return BadRequest("There is no user with this ID!");

            var result = await userManager.SetLockoutEndDateAsync(user, null);
            if (result.Succeeded) return Ok("User locked!");
            return BadRequest("Error during unlock!");
        }

        [HttpPost("user/change-role")]
        public async Task<IActionResult> ChangeRole(
            [FromBody] RolesManagementForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userClaims = await userManager.GetClaimsAsync(user) as List<Claim>;
            var roleClaim = userClaims?.FirstOrDefault(x => x.Type.Equals(SystemyWPConstants.Claims.Role));

            if (!form.Role.Equals(roleClaim?.Value, StringComparison.InvariantCultureIgnoreCase))
            {
                var logoutResult = await userManager.UpdateSecurityStampAsync(user);
                if (logoutResult.Succeeded)
                {
                    var roleRemoveResult = await userManager.RemoveClaimAsync(user, roleClaim);
                    if (roleRemoveResult.Succeeded)
                    {
                        if (form.Role.Equals(
                            SystemyWPConstants.Roles.Client, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var addToRoleResult =
                                await userManager.AddClaimAsync(user, SystemyWPConstants.Claims.ClientClaim);
                            if (addToRoleResult.Succeeded) return Ok();
                        }
                        else if (form.Role.Equals(
                            SystemyWPConstants.Roles.ClientAdmin, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var addToRoleResult =
                                await userManager.AddClaimAsync(user, SystemyWPConstants.Claims.ClientAdminClaim);
                            if (addToRoleResult.Succeeded) return Ok();
                        }
                        else
                        {
                            return BadRequest("Unable to add to new role!");
                        }
                    }
                    else
                    {
                        return BadRequest("Unable to remove from role!");
                    }
                }
                else
                {
                    return BadRequest("Unable to sign out user!");
                }
            }
            else
            {
                return BadRequest("User already in this role!");
            }

            return Ok();
        }

        [HttpPost("user/delete")]
        public IActionResult DeleteUser(string userId)
        {
            //todo: do this
            return Ok();
        }

        [HttpPost("user/grant/legal-app")]
        public async Task<IActionResult> GrantLegalAppAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var legalAppClaim = await userManager.GetClaimsAsync(user) as List<Claim>;

            if (user is null || legalAppClaim is null) return BadRequest("User not found!");

            if (legalAppClaim.Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess) &&
                                       x.Value.Equals(SystemyWPConstants.Apps.LegalApp)))
                return BadRequest("User already have access!");

            var result = await userManager
                .AddClaimAsync(user, SystemyWPConstants.Claims.LegalAppAccessClaim);

            if (result.Succeeded) return Ok("Claim Added!");

            return BadRequest("Error when adding claim!");
        }

        [HttpPost("user/revoke/legal-app")]
        public async Task<IActionResult> RevokeLegalAppAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var legalAppClaim = await userManager.GetClaimsAsync(user) as List<Claim>;

            if (user is null || legalAppClaim is null) return BadRequest("User not found!");

            if (!legalAppClaim.Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess) &&
                                        x.Value.Equals(SystemyWPConstants.Apps.LegalApp)))
                return BadRequest("User already does not have access!");

            var result = await userManager
                .RemoveClaimAsync(user, SystemyWPConstants.Claims.LegalAppAccessClaim);

            if (result.Succeeded) return Ok("Claim Removed!");

            return BadRequest("Error when removing claim!");
        }
    }
}