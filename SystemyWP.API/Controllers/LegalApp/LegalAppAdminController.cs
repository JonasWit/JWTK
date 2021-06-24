﻿using System;
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
using SystemyWP.Data.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    //todo: delete in the future
    [Route("/api/legal-app-admin")]
    [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
    public class LegalAppAdminController : ApiController
    {
        public LegalAppAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("related-users")]
        public async Task<IActionResult> GetRelatedUsers([FromServices] UserManager<IdentityUser> userManager)
        {
            var result = new List<object>();

            try
            {
                //Get current admin who made request
                var adminUser = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);

                if (adminUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                if (adminUser.LegalAppAccessKey is null) return Ok(result);

                //Get related users with the same data access key
                var relatedUsers = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .Where(x => x.LegalAppAccessKey.Id == adminUser.LegalAppAccessKey.Id)
                    .Include(x => x.DataAccess)
                    .ToList();

                if (relatedUsers.Count == 0) return Ok(result);

                foreach (var relatedUser in relatedUsers)
                {
                    var userRecord = await userManager.FindByIdAsync(relatedUser.Id);
                    var role = (await userManager.GetClaimsAsync(userRecord) as List<Claim>)?
                        .FirstOrDefault(x => x.Type.Equals(SystemyWpConstants.Claims.Role))?
                        .Value;
                    result.Add(UserProjections
                        .RelatedLegalAppUserProjection(userRecord.UserName, userRecord.Email, role)
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

        [HttpPost("update-legal-app-data-access")]
        public async Task<IActionResult> UpdateLegalAppDataAccess(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] LegalAppUpdateUserAccessForm form)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(form.UserId);
                if (identityUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var logoutResult = await userManager.UpdateSecurityStampAsync(identityUser);
                if (!logoutResult.Succeeded)  return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var requester = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (requester is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                var user = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                if (requester.LegalAppAccessKey is null || user.LegalAppAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                if (!requester.LegalAppAccessKey.Name.Equals(user.LegalAppAccessKey.Name)) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                _context.DataAccesses.RemoveRange(_context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                foreach (var allowedClient in form.AllowedClients)
                    _context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppClient,
                        ItemId = allowedClient,
                        UserId = user.Id,
                        CreatedBy = UserId
                    });

                foreach (var allowedCase in form.AllowedCases)
                    _context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppCase,
                        ItemId = allowedCase,
                        UserId = user.Id,
                        CreatedBy = UserId
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

        [HttpPost("full-legal-app-data-access")]
        public async Task<IActionResult> GrantFullDataAccess(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] LegalAppUpdateUserAccessForm form)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(form.UserId);
                if (identityUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var logoutResult = await userManager.UpdateSecurityStampAsync(identityUser);
                if (!logoutResult.Succeeded) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var requester = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (requester is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                var user = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                if (requester.LegalAppAccessKey is null || user.LegalAppAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                if (!requester.LegalAppAccessKey.Name.Equals(user.LegalAppAccessKey.Name)) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                _context.DataAccesses.RemoveRange(_context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                var clients = _context.LegalAppClients
                    .Include(x => x.LegalAppCases)
                    .Include(x => x.LegalAppAccessKey)
                    .Where(x =>
                        x.LegalAppAccessKey.Name.Equals(requester.LegalAppAccessKey.Name))
                    .ToList();

                foreach (var client in clients)
                {
                    _context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppClient,
                        ItemId = client.Id,
                        UserId = user.Id,
                        CreatedBy = UserId
                    });

                    foreach (var cs in client.LegalAppCases)
                        _context.Add(new DataAccess
                        {
                            RestrictedType = RestrictedType.LegalAppCase,
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

        [HttpPost("revoke-legal-app-data-access")]
        public async Task<IActionResult> RevokeAllDataAccess(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromBody] LegalAppUpdateUserAccessForm form)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(form.UserId);
                if (identityUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var logoutResult = await userManager.UpdateSecurityStampAsync(identityUser);
                if (!logoutResult.Succeeded) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                var requester = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (requester is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                var user = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                
                if (requester.LegalAppAccessKey is null || user.LegalAppAccessKey is null) return BadRequest();
                if (!requester.LegalAppAccessKey.Name.Equals(user.LegalAppAccessKey.Name)) return BadRequest();

                _context.DataAccesses.RemoveRange(_context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("legal-app-summary")]
        public async Task<IActionResult> GetLegalAppSummary([FromServices] UserManager<IdentityUser> userManager)
        {
            var result = new AppSummaryViewModel();

            var accessKey = await _context.LegalAppAccessKeys
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Users.Any(y => y.Id.Equals(UserId)));

            if (accessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

            var relatedUsers = _context.Users
                .Include(x => x.LegalAppAccessKey)
                .Where(x => x.LegalAppAccessKey.Name.Equals(accessKey.Name))
                .ToList();

            var appData = _context.LegalAppClients
                .Include(x => x.LegalAppAccessKey)
                .Where(x => x.LegalAppAccessKey.Name.Equals(accessKey.Name))
                .Select(x => new
                {
                    x.Id,
                    x.LegalAppCases.Count
                })
                .ToList();

            result.ClientsCount = appData.Count;
            result.CasesCount = appData.Sum(x => x.Count);
            result.UsersCount = relatedUsers.Count;

            return Ok();
        }
    }
}