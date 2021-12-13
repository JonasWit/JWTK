﻿using System;
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
using SystemyWP.Data.Models.RestaurantAppModels.Access;

namespace SystemyWP.API.Controllers.RestaurantApp.RestaurantAppManagement
{
    [Route("/api/portal-admin/key-admin/restaurant-app")]
    [Authorize]
    public class RestaurantAppAccessKeyController : ApiController
    {
        public RestaurantAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("access-keys")]
        [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
        public async Task<IActionResult> ListAccessKeys()
        {
            try
            {
                var results = _context.RestaurantAccessKeys
                    .Include(x => x.Users)
                    .Select(AccessKeyProjection.FullProjection)
                    .ToList();

                if (results.Count == 0) return NotFound();
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
                if (_context.RestaurantAccessKeys.Any(x => x.Name.ToLower().Equals(form.KeyName.ToLower())))
                    return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                _context.Add(new RestaurantAccessKey
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
                var keyToUpdate = _context.RestaurantAccessKeys
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
                    .Include(x => x.RestaurantAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user?.RestaurantAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.RestaurantAccessKeys.Remove(user.RestaurantAccessKey);
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
                var keyToDelete = _context.RestaurantAccessKeys
                    .FirstOrDefault(x => x.Id == id);
                if (keyToDelete is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                _context.RestaurantAccessKeys.Remove(keyToDelete);

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
        public async Task<IActionResult> GrantAccessKey([FromBody] GrantDataAccessKeyForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (user is null || userProfile is null)
                    return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var accessKey = _context.RestaurantAccessKeys
                    .FirstOrDefault(x => x.Id == form.KeyId);
                if (accessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                accessKey.Users.Add(userProfile);
                var result = await _context.SaveChangesAsync();

                if (result > 0) return Ok();
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
        public async Task<IActionResult> RevokeAccessKey([FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(form.UserId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null)
                    return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var assignedKey = _context.RestaurantAccessKeys
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                if (assignedKey is not null)
                {
                    assignedKey.Users.RemoveAll(x => x.Id.Equals(user.Id));
                    _context.RemoveRange(_context.RestaurantAppDataAccesses.Where(x => x.UserId.Equals(form.UserId)));

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