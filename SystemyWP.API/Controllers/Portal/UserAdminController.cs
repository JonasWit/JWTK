using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Projections.LegalApp.LegalAppAdmin;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-admin/user-admin")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class UserAdminController : ApiController
    {
        public UserAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("users")]
        public async Task<IActionResult> ListUsers(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var identityUsers = userManager.Users.ToList();

                var result = new List<UserProjections.UserViewModel>();
                foreach (var identityUser in identityUsers)
                {
                    var userClaims = await userManager.GetClaimsAsync(identityUser) as List<Claim>
                                     ?? new List<Claim>();

                    var accessKey = _context.LegalAppAccessKeys
                        .Include(x => x.Users)
                        .Where(x => x.Users.Any(y => y.Id.Equals(identityUser.Id)))
                        .Select(AccessKeyProjection.FullProjection)
                        .FirstOrDefault();

                    result.Add(new UserProjections.UserViewModel
                    {
                        Id = identityUser.Id,
                        Username = identityUser.UserName,
                        Email = identityUser.Email,
                        LegalAppAllowed = userClaims
                            .Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess)),
                        Image = _context.Users
                            .FirstOrDefault(x => x.Id.Equals(identityUser.Id))?.Image,
                        LegalAppDataAccessKey = accessKey,
                        Role = userClaims
                            .FirstOrDefault(x =>
                                x.Type.Equals(SystemyWpConstants.Claims.Role))?.Value,
                        Locked = identityUser.LockoutEnd is not null,
                        EmailConfirmed = identityUser.EmailConfirmed
                    });
                }

                return Ok(result.OrderByDescending(x => x.Email).ToList());
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/lock")]
        public async Task<IActionResult> LockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                if (user is null) return BadRequest("There is no user with this ID!");

                var result = await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(+25));
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    return Ok("User locked!");
                }

                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/unlock")]
        public async Task<IActionResult> UnlockUser(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                if (user is null) return BadRequest("There is no user with this ID!");

                var result = await userManager.SetLockoutEndDateAsync(user, null);
                if (result.Succeeded) return Ok("User locked!");
                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/change-role")]
        public async Task<IActionResult> ChangeRole(
            [FromBody] RolesManagementForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userClaims = await userManager.GetClaimsAsync(user) as List<Claim>;
                var roleClaim = userClaims?.FirstOrDefault(x => x.Type.Equals(SystemyWpConstants.Claims.Role));

                if (form.Role.Equals(roleClaim?.Value, StringComparison.InvariantCultureIgnoreCase))
                    return BadRequest("Role Claim not found!");

                var logoutResult = await userManager.UpdateSecurityStampAsync(user);
                if (!logoutResult.Succeeded)
                    return BadRequest("Unable to sign out user!");

                //Remove from current role
                var roleRemoveResult = await userManager.RemoveClaimAsync(user, roleClaim);
                if (!roleRemoveResult.Succeeded)
                    return BadRequest("Unable to remove from role!");

                //Change from admin to normal
                if (form.Role.Equals(SystemyWpConstants.Roles.User, StringComparison.InvariantCultureIgnoreCase))
                {
                    var addToRoleResult =
                        await userManager.AddClaimAsync(user, SystemyWpConstants.Claims.UserClaim);
                    if (addToRoleResult.Succeeded) return Ok();
                }
                
                //Change from normal to admin
                if (form.Role.Equals(SystemyWpConstants.Roles.UserAdmin, StringComparison.InvariantCultureIgnoreCase))
                {
                    var addToRoleResult =
                        await userManager.AddClaimAsync(user, SystemyWpConstants.Claims.UserAdminClaim);
                    if (addToRoleResult.Succeeded)
                    {
                        _context.RemoveRange(_context.LegalAppDataAccesses.Where(x => x.UserId.Equals(user.Id)));
                        _context.RemoveRange(_context.MedicalAppDataAccesses.Where(x => x.UserId.Equals(user.Id)));
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    if (addToRoleResult.Succeeded) return Ok();
                }

                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IWebHostEnvironment env)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                if (user is null) return BadRequest();

                var lockResult = await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(+25));
                var logoutResult = await userManager.UpdateSecurityStampAsync(user);

                if (lockResult.Succeeded && logoutResult.Succeeded)
                {
                    var userProfile = await _context.Users
                        .FirstOrDefaultAsync(x => x.Id == form.UserId);

                    if (userProfile is not null)
                    {
                        _context.Remove(userProfile);
                        await _context.SaveChangesAsync();
                    }
                    
                    var deleteResult = await userManager.DeleteAsync(user);
                    if (deleteResult.Succeeded)
                    {
                        return Ok();
                    }
                }

                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/grant/legal-app")]
        public async Task<IActionResult> GrantLegalAppAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var legalAppClaim = await userManager.GetClaimsAsync(user) as List<Claim>;

                if (user is null || legalAppClaim is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                if (legalAppClaim.Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) &&
                                           x.Value.Equals(SystemyWpConstants.Apps.LegalApp)))
                    return BadRequest(SystemyWpConstants.ResponseMessages.AlreadyGranted);

                var result = await userManager
                    .AddClaimAsync(user, SystemyWpConstants.Claims.LegalAppAccessClaim);

                if (result.Succeeded) return Ok("Claim Added!");

                return BadRequest("Error when adding claim!");
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/revoke/legal-app")]
        public async Task<IActionResult> RevokeLegalAppAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var legalAppClaim = await userManager.GetClaimsAsync(user) as List<Claim>;

                if (user is null || legalAppClaim is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                if (!legalAppClaim.Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) &&
                                            x.Value.Equals(SystemyWpConstants.Apps.LegalApp)))
                    return BadRequest("User already does not have access!");

                var result = await userManager
                    .RemoveClaimAsync(user, SystemyWpConstants.Claims.LegalAppAccessClaim);

                if (result.Succeeded) return Ok("Claim Removed!");

                return BadRequest("Error when removing claim!");
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}