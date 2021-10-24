using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Forms.GeneralApp.Access;
using SystemyWP.API.Projections.General;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.MedicalAppModels.Access;

namespace SystemyWP.API.Controllers.MedApp.MedicalAppManagement
{
    [Route("/api/portal-admin/key-admin/medical-app")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class MedicalAppAccessKeyController : ApiController
    {
        public MedicalAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("access-keys")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> ListAccessKeys()
        {
            try
            {
                var results = _context.MedicalAccessKeys
                    .Include(x => x.Users)
                    .Select(AccessKeyProjection.FullProjection)
                    .ToList();
                
                return Ok(results);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("access-key/create")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> CreateAccessKey([FromBody] CreateAccessKeyForm form)
        {
            try
            {
                if (_context.MedicalAccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
                    return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                _context.Add(new MedicalAccessKey()
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
                return ServerError;
            }
        }

        [HttpPut("access-key/update/{keyId}")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> UpdateAccessKey(int keyId, [FromBody] EditAccessKeyForm form)
        {
            try
            {
                var keyToUpdate = _context.MedicalAccessKeys
                    .FirstOrDefault(x => x.Id == keyId);
                if (keyToUpdate is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

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
                return ServerError;
            }
        }

        [HttpGet("access-key/delete")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> DeleteAccessKey()
        {
            try
            {
                var user = await _context.Users
                    .Include(x => x.MedicalAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user?.MedicalAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.MedicalAccessKeys.Remove(user.MedicalAccessKey);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpDelete("access-key/delete/{id}")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> DeleteAccessKey(int id)
        {
            try
            {
                var keyToDelete = _context.MedicalAccessKeys
                    .FirstOrDefault(x => x.Id == id);
                if (keyToDelete is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                _context.MedicalAccessKeys.Remove(keyToDelete);

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("user/grant/access-key")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> GrantDataAccessKey([FromBody] GrantDataAccessKeyForm form, [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (user is null || userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var accessKey = _context.MedicalAccessKeys
                    .FirstOrDefault(x => x.Id == form.KeyId);
                if (accessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                accessKey.Users.Add(userProfile);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                    return Ok();
                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpPost("access-key/revoke")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> RevokeDataAccessKey([FromBody] UserIdForm form, [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var assignedKey = _context.MedicalAccessKeys
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                if (assignedKey is not null)
                {
                    assignedKey.Users.RemoveAll(x => x.Id.Equals(user.Id));
                    _context.RemoveRange(_context.MedicalAppDataAccesses.Where(x => x.UserId.Equals(form.UserId)));

                    var result = await _context.SaveChangesAsync();

                    if (result > 0) return Ok();
                    return BadRequest();
                }
                
                return BadRequest();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}