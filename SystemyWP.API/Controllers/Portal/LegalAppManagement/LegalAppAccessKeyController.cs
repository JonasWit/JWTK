using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.GeneralApp.Access;
using SystemyWP.API.Projections.LegalApp.LegalAppAdmin;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal.LegalAppManagement
{
    [Route("/api/portal-admin/key-admin/legal-app")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class LegalAppAccessKeyController : ApiController
    {
        public LegalAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("access-keys")]
        public async Task<IActionResult> ListAccessKeys()
        {
            try
            {
                var results = _context.LegalAppAccessKeys
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
        public async Task<IActionResult> CreateAccessKey([FromBody] CreateAccessKeyForm form)
        {
            try
            {
                if (_context.LegalAppAccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
                    return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

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
                return ServerError;
            }
        }

        [HttpPut("access-key/update/{keyId}")]
        public async Task<IActionResult> UpdateAccessKey(int keyId, [FromBody] EditAccessKeyForm form)
        {
            try
            {
                var keyToUpdate = _context.LegalAppAccessKeys
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

        [HttpDelete("access-key/delete/{id}")]
        public async Task<IActionResult> DeleteAccessKey(int id)
        {
            try
            {
                var keyToDelete = _context.LegalAppAccessKeys
                    .FirstOrDefault(x => x.Id == id);
                if (keyToDelete is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                _context.LegalAppAccessKeys.Remove(keyToDelete);
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
        public async Task<IActionResult> GrantDataAccessKey(
            [FromBody] GrantDataAccessKeyForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (user is null || userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var accessKey = _context.LegalAppAccessKeys
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
    }
}