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
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers
{
    [Route("/api/portal-admin")]
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
            return Ok(_context.AccessKeys.ToList());
        }

        [HttpPost("client/grant/access-key")]
        public async Task<IActionResult> GrantDataAccessKey(
            [FromBody] GrantDataAccessKey form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

            if (user is null || userProfile is null)
            {
                return BadRequest("There is no user with this ID!");
            }

            userProfile.AccessKeyId = form.DataAccessKey;

            _context.Users.Update(userProfile);
            
            var result =  await _context.SaveChangesAsync();
            
            if (result > 0)
            {
                return Ok($"Data Access Key {form.DataAccessKey} Added!");
            }
            else
            {
                return BadRequest("Error when adding the Claim!");
            }
        }

        [HttpPost("client/revoke/access-key")]
        public async Task<IActionResult> RevokeDataAccessKey(
            [FromBody] RevokeDataAccessKey form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

            if (user is null || userProfile is null)
            {
                return BadRequest("There is no user with this ID!");
            }

            var assignedKey = _context.AccessKeys
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Users.Any(x => x.Id.Equals(user.Id)));

            if (assignedKey is not null)
            {
                assignedKey.Users.RemoveAll(x => x.Id.Equals(user.Id));

                _context.Update(assignedKey);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok($"Data Access Key Removed!");
                }
            }
            else
            {
                return BadRequest("Key not found!");      
            }

            return BadRequest("Error when removing the access key!");
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
                var userClaims = (await userManager.GetClaimsAsync(identityUser) as List<Claim>) 
                                 ?? new List<Claim>();
                var userProfile = _context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(identityUser.Id));
                
                result.Add(new UserProjections.UserViewModel
                {
                    Id = identityUser.Id,
                    Username = identityUser.UserName,
                    Email = identityUser.Email,
                    LegalAppAllowed = userClaims
                        .Any(x => x.Type.Equals(SystemyWPConstants.Claims.LegalAppAccess)),
                    Image = context.Users
                        .FirstOrDefault(x => x.Id.Equals(identityUser.Id))?.Image,
                    DataAccessKey = userProfile.AccessKey?.Id,
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

            if (user is null)
            {
                return BadRequest("There is no user with this ID!");
            }
            
            var result = await userManager.SetLockoutEndDateAsync(user, null);

            if (result.Succeeded) return Ok("User locked!");
            return BadRequest("Error during unlock!");
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
                code,
            }, protocol: HttpContext.Request.Scheme);

            await emailClient.SendClientInvite(form.Email, link);

            return Ok(link);
        }

        [HttpPost("user/change-role")]
        public async Task<IActionResult> ChangeRole(
            [FromBody] RolesManagementForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userClaims = await userManager.GetClaimsAsync(user) as List<Claim>;
            var roleClaim = userClaims.FirstOrDefault(x => x.Type.Equals(SystemyWPConstants.Claims.Role));

            if (!form.Role.Equals(roleClaim.Value, StringComparison.InvariantCultureIgnoreCase))
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
                            if (addToRoleResult.Succeeded)
                            {
                                return Ok();
                            }
                        }
                        else if (form.Role.Equals(
                            SystemyWPConstants.Roles.ClientAdmin, StringComparison.InvariantCultureIgnoreCase))
                        {
                            var addToRoleResult = 
                                await userManager.AddClaimAsync(user, SystemyWPConstants.Claims.ClientAdminClaim);
                            if (addToRoleResult.Succeeded)
                            {
                                return Ok();
                            }
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
        public async Task<IActionResult> DeleteUser(string userId)
        {



            return Ok();
        }
        
    }
}