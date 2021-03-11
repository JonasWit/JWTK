using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers
{
    [Route("/api/portal-admin/key-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class AccessKeyAdminController : ApiController
    {
        [HttpGet("access-keys")]
        public IEnumerable<object> ListAccessKeys([FromServices] AppDbContext context) =>
            context.AccessKeys
                .Include(x => x.Users)
                .Select(AccessKeyProjection.Projection)
                .ToList();

        [HttpPost("access-key/create")]
        public async Task<IActionResult> CreateAccessKey(
            [FromServices] AppDbContext context,
            [FromBody] AccessKeyForm form)
        {
            if (context.AccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
            {
                return BadRequest("Key with this name already exists!");
            }
            
            await _portalLogger.Log(LogType.PortalAdminAction, $"New Access Key requested: {form.KeyName}", UserId, Username);

            context.Add(new AccessKey
            {
                Name = form.KeyName,
                ExpireDate = form.ExpireDate
            });

            await context.SaveChangesAsync();
            await _portalLogger.Log(LogType.Access, $"Data Access Key {form.KeyName} added by {Username}.", UserId, Username);
            return Ok();
        }

        [HttpPut("access-key/update")]
        public async Task<IActionResult> UpdateAccessKey(
            [FromServices] AppDbContext context,
            [FromBody] EditAccessKeyForm form)
        {
            var keyToUpdate = context.AccessKeys
                .FirstOrDefault(x => x.Name.ToLower().Equals(form.OldKeyName.ToLower()));

            if (keyToUpdate is null)
            {
                return BadRequest("Key with this name not exists!");
            }
            
            await _portalLogger.Log(LogType.PortalAdminAction, $"Access Key updated from: {keyToUpdate.Name} to: {form.NewKeyName}", UserId, Username);

            keyToUpdate.Name = form.NewKeyName;
            keyToUpdate.ExpireDate = form.ExpireDate;

            context.Update(keyToUpdate);
            await context.SaveChangesAsync();
            await _portalLogger.Log(LogType.Access, $"Data Access Key {form.NewKeyName} updated by {Username}.", UserId, Username);
            return Ok();
        }

        [HttpDelete("access-key/{id}")]
        public async Task<IActionResult> DeleteAccessKey(
            [FromServices] AppDbContext context, int id)
        {
            var keyToDelete = context.AccessKeys
                .FirstOrDefault(x => x.Id == id);

            if (keyToDelete is null)
            {
                return BadRequest("Key with this id does not exists!");
            }
            
            await _portalLogger.Log(LogType.PortalAdminAction, $"Access Key delete requested for: {keyToDelete.Name}", UserId, Username);

            context.AccessKeys.Remove(keyToDelete);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user/grant/access-key")]
        public async Task<IActionResult> GrantDataAccessKey(
            [FromServices] AppDbContext context,
            [FromBody] GrantDataAccessKeyForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userProfile = context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

            if (user is null || userProfile is null)
            {
                return BadRequest("There is no user with this ID!");
            }
            
            var accessKey = context.AccessKeys
                .FirstOrDefault(x => x.Name.ToLower().Equals(form.DataAccessKey.ToLower()));
            
            await _portalLogger.Log(LogType.PortalAdminAction, $"Access Key {accessKey.Name} Requested for {user.Email}", UserId, Username);

            accessKey.Users.Add(userProfile);
            context.Users.Update(userProfile);

            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok($"Data Access Key {form.DataAccessKey} Added!");
            }
            else
            {
                return BadRequest("Error when adding the Claim!");
            }
        }

        [HttpPost("user/revoke/access-key")]
        public async Task<IActionResult> RevokeDataAccessKey(
            [FromBody] RevokeDataAccessKeyForm form,
            [FromServices] AppDbContext context,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(form.UserId);
            var userProfile = context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

            if (user is null || userProfile is null)
            {
                return BadRequest("There is no user with this ID!");
            }

            var assignedKey = context.AccessKeys
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Users.Any(x => x.Id.Equals(user.Id)));
            
            await _portalLogger
                .Log(LogType.PortalAdminAction, $"Access Key {assignedKey.Name} Revoke for {user.Email}", UserId, Username);

            if (assignedKey is not null)
            {
                assignedKey.Users.RemoveAll(x => x.Id.Equals(user.Id));

                context.Update(assignedKey);
                var result = await context.SaveChangesAsync();

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

        public AccessKeyAdminController(PortalLogger portalLogger) : base(portalLogger)
        {
        }
    }
}