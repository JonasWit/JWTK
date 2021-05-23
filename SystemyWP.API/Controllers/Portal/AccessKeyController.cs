﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-admin/key-admin")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class AccessKeyController : ApiController
    {
        public AccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("access-keys")]
        public async Task<ActionResult<IEnumerable<object>>> ListAccessKeys()
        {
            try
            {
                var legalAppRelatedDataCount = await _context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .GroupBy(x => x.AccessKey.Name)
                    .Select(x => new
                    {
                        KeyName = x.Key,
                        Count = x.Select(y => y.Id).Distinct().Count()
                    })
                    .ToListAsync();

                var results = _context.AccessKeys
                    .Include(x => x.Users)
                    .AsEnumerable()
                    .Select(x => AccessKeyProjection
                        .FullProjection(
                            legalAppRelatedDataCount.Any(y => y.KeyName.Equals(x.Name))
                                ? legalAppRelatedDataCount
                                    .First(y => y.KeyName.Equals(x.Name))
                                    .Count
                                : 0, x.Users)
                        .Compile()
                        .Invoke(x))
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
        public async Task<IActionResult> CreateAccessKey([FromBody] AccessKeyForm form)
        {
            try
            {
                if (_context.AccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
                    return BadRequest("Key with this name already exists!");

                _context.Add(new AccessKey
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

        [HttpPut("access-key/update")]
        public async Task<IActionResult> UpdateAccessKey([FromBody] EditAccessKeyForm form)
        {
            try
            {
                var keyToUpdate = _context.AccessKeys
                    .FirstOrDefault(x => x.Name.ToLower().Equals(form.OldKeyName.ToLower()));

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
                var keyToDelete = _context.AccessKeys
                    .FirstOrDefault(x => x.Id == id);

                if (keyToDelete is null) return BadRequest("Key with this id does not exists!");

                var relatedLegalAppData = _context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Count(x => x.AccessKey.Name.Equals(keyToDelete.Name));

                if (relatedLegalAppData > 0)
                    _context.LegalAppClients.RemoveRange(_context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Name.Equals(keyToDelete.Name)));

                _context.AccessKeys.Remove(keyToDelete);
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

                var accessKey = _context.AccessKeys
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

                var assignedKey = _context.AccessKeys
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