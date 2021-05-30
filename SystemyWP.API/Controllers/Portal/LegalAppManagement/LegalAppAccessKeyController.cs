using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.GeneralApp.Access;
using SystemyWP.API.Projections.LegalApp.LegalAppAdmin;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal.LegalAppManagement
{
    [Route("/api/portal-admin/key-admin")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class LegalAppAccessKeyController : ApiController
    {
        public LegalAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("access-keys")]
        public async Task<ActionResult<IEnumerable<object>>> ListAccessKeys()
        {
            try
            {
                var legalAppRelatedDataCount = await _context.LegalAppClients
                    .Include(x => x.LegalAppAccessKey)
                    .GroupBy(x => x.LegalAppAccessKey.Name)
                    .Select(x => new
                    {
                        KeyName = x.Key,
                        Count = x.Select(y => y.Id).Distinct().Count()
                    })
                    .ToListAsync();

                var results = _context.LegalAppAccessKeys
                    .Include(x => x.Users)
                    .Select(LegalAppAccessKeyProjection.FullProjection())
                    .ToList();
                return Ok(results);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest();  
            }
        }

        [HttpPost("access-key/create")]
        public async Task<IActionResult> CreateAccessKey([FromBody] CreateAccessKeyForm form)
        {
            try
            {
                if (_context.LegalAppAccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
                    return BadRequest("Key with this name already exists!");

                _context.Add(new LegalAppAccessKey
                {
                    Name = form.KeyName,
                    ExpireDate = form.ExpireDate,
                    CreatedBy = UserId,
                    UpdatedBy = UserId
                });

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest();  
            }
        }

        [HttpPut("access-key/update/{id}")]
        public async Task<IActionResult> UpdateAccessKey(int keyId, [FromBody] EditAccessKeyForm form)
        {
            try
            {
                var keyToUpdate = _context.LegalAppAccessKeys
                    .FirstOrDefault(x => x.Id == keyId);

                if (keyToUpdate is null) return BadRequest("Key with this name not exists!");

                keyToUpdate.Name = form.NewKeyName;
                keyToUpdate.ExpireDate = form.ExpireDate;
                keyToUpdate.UpdatedBy = UserId;

                _context.Update(keyToUpdate);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest(); 
            }
        }

        [HttpDelete("access-key/delete/{id}")]
        public async Task<IActionResult> DeleteAccessKey(int id)
        {
            try
            {
                var keyToDelete = _context.LegalAppAccessKeys
                    .FirstOrDefault(x => x.Id == id);

                if (keyToDelete is null) return BadRequest("Key with this id does not exists!");

                var relatedLegalAppData = _context.LegalAppClients
                    .Include(x => x.LegalAppAccessKey)
                    .Count(x => x.LegalAppAccessKey.Name.Equals(keyToDelete.Name));

                if (relatedLegalAppData > 0)
                    _context.LegalAppClients.RemoveRange(_context.LegalAppClients
                        .Include(x => x.LegalAppAccessKey)
                        .Where(x => x.LegalAppAccessKey.Name.Equals(keyToDelete.Name)));

                _context.LegalAppAccessKeys.Remove(keyToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest(); 
            }
        }

        [HttpPost("user/grant/access-key")]
        public async Task<IActionResult> GrantDataAccessKey(
            [FromBody] GrantDataAccessKeyForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null) return BadRequest("There is no user with this ID!");

                var accessKey = _context.LegalAppAccessKeys
                    .FirstOrDefault(x => x.Name.ToLower().Equals(form.DataAccessKey.ToLower()));

                if (accessKey is null) return BadRequest("Key not found!");

                accessKey.Users.Add(userProfile);
                _context.Users.Update(userProfile);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                    return Ok($"Data Access Key {form.DataAccessKey} Added!");
                return BadRequest("Error when adding the Claim!");   
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest("Error when adding the Claim!");   
            }
        }

        [HttpPost("user/revoke/access-key")]
        public async Task<IActionResult> RevokeDataAccessKey(
            [FromBody] RevokeDataAccessKeyForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null) return BadRequest("There is no user with this ID!");

                var assignedKey = _context.LegalAppAccessKeys
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                if (assignedKey is not null)
                {
                    assignedKey.Users.RemoveAll(x => x.Id.Equals(user.Id));

                    _context.Update(assignedKey);
                    var result = await _context.SaveChangesAsync();

                    if (result > 0) return Ok("Data Access Key Removed!");
                    return BadRequest();
                }
                else
                {
                    return BadRequest("Key not found!");
                }
            }
            catch (Exception e)
            {
                await HandleException(e);
                return BadRequest();  
            }
        }
    }
}