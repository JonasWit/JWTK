﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;

namespace SystemyWP.API.Controllers.LegalApp.LegalAppManagement
{
    [Route("/api/legal-app-admin/general")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class LegalAppAdminController : ApiController
    {
        public LegalAppAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("all-related-users")]
        public async Task<IActionResult> GetRelatedUsers([FromServices] UserManager<IdentityUser> userManager)
        {
            var result = new List<object>();

            try
            {
                //Get current admin who made request
                var adminUser = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (adminUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                if (adminUser.LegalAccessKey is null) return Ok(result);

                //Get related users with the same data access key
                var relatedUsers = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .Where(x => x.LegalAccessKey.Id == adminUser.LegalAccessKey.Id)
                    .Include(x => x.LegalAppDataAccesses)
                    .ToList();

                if (relatedUsers.Count == 0) return Ok(result);

                //Get info about current roles
                foreach (var relatedUser in relatedUsers)
                {
                    var userRecord = await userManager.FindByIdAsync(relatedUser.Id);
                    var role = (await userManager.GetClaimsAsync(userRecord) as List<Claim>)?
                        .FirstOrDefault(x => x.Type.Equals(SystemyWpConstants.Claims.Role))?
                        .Value;
                    result.Add(UserProjections
                        .RelatedUserProjection(role)
                        .Compile()
                        .Invoke(relatedUser));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("grant/full-legal-app-data-access")]
        public async Task<IActionResult> GrantFullDataAccess(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(form.UserId);
                if (identityUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var logoutResult = await userManager.UpdateSecurityStampAsync(identityUser);
                if (!logoutResult.Succeeded) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var requester = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (requester is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                var user = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                if (requester.LegalAccessKey is null || user.LegalAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                if (requester.LegalAccessKey.Id != user.LegalAccessKey.Id) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.LegalAppDataAccesses
                    .RemoveRange(_context.LegalAppDataAccesses
                        .Where(x => 
                            x.UserId.Equals(user.Id)));

                var clients = _context.LegalAppClients
                    .Include(x => x.LegalAppCases)
                    .GetAllowedClients(UserId, Role, _context, true)
                    .ToList();

                foreach (var client in clients)
                {
                    _context.Add(new LegalAppDataAccess
                    {
                        LegalAppRestrictedType = LegalAppRestrictedType.LegalAppClient,
                        ItemId = client.Id,
                        UserId = user.Id,
                        CreatedBy = UserId
                    });

                    foreach (var cs in client.LegalAppCases)
                        _context.Add(new LegalAppDataAccess
                        {
                            LegalAppRestrictedType = LegalAppRestrictedType.LegalAppCase,
                            ItemId = cs.Id,
                            UserId = user.Id,
                            CreatedBy = UserId
                        });
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("revoke/full-legal-app-data-access")]
        public async Task<IActionResult> RevokeAllDataAccess(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] UserIdForm form)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(form.UserId);
                if (identityUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var logoutResult = await userManager.UpdateSecurityStampAsync(identityUser);
                if (!logoutResult.Succeeded) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var requester = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (requester is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var user = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                if (requester.LegalAccessKey is null || user.LegalAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                if (requester.LegalAccessKey.Id != user.LegalAccessKey.Id) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.LegalAppDataAccesses
                    .RemoveRange(_context.LegalAppDataAccesses
                        .Where(x => 
                            x.UserId.Equals(user.Id)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}