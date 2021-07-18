﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-client-access")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class LegalAppClientAccessController : LegalAppApiController
    {
        public LegalAppClientAccessController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/allowed-users")]
        public async Task<IActionResult> GetClientAllowedUsers(long clientId)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (legalAppClient is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var allowedUsers = _context.LegalAppDataAccesses
                    .Where(x => x.ItemId == clientId && x.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient)
                    .Select(x => x.User)
                    .Select(UserProjections.BasicUserProjection)
                    .ToList();

                return Ok(allowedUsers);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("client/{clientId}/grant-access")]
        public async Task<IActionResult> GrantAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            long clientId)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (legalAppClient is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (admin is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                // Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await GetOnlyNormalUsers(users, userManager);
                if (!result.Any(userBasic => userBasic.Id.Equals(form.UserId)))
                    return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var currentAccesses = _context.LegalAppDataAccesses
                    .Where(dataAccess =>
                        dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
                        dataAccess.UserId.Equals(form.UserId))
                    .ToList();
                if (currentAccesses.Any(dataAccess => dataAccess.ItemId == clientId))
                    return BadRequest(SystemyWpConstants.ResponseMessages.AlreadyGranted);

                _context.LegalAppDataAccesses.Add(new LegalAppDataAccess
                {
                    LegalAppAccessKey = admin.LegalAppAccessKey,
                    UserId = form.UserId,
                    CreatedBy = UserEmail,
                    ItemId = clientId,
                    LegalAppRestrictedType = LegalAppRestrictedType.LegalAppClient
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

        [HttpPost("client/{clientId}/revoke-access")]
        public async Task<IActionResult> RevokeAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            long clientId)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);

                //Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await GetOnlyNormalUsers(users, userManager);
                if (!result.Any(x => x.Id.Equals(form.UserId)))
                    return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);

                //Remove client access
                _context.RemoveRange(
                    _context.LegalAppDataAccesses
                        .Where(x =>
                            x.ItemId == clientId &&
                            x.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
                            x.UserId.Equals(form.UserId)));

                //Remove cases access
                _context.RemoveRange(
                    _context.LegalAppDataAccesses
                        .Where(dataAccess =>
                            dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase &&
                            dataAccess.UserId.Equals(form.UserId) &&
                            _context.LegalAppCases.GetAllowedCases(UserId, Role, clientId, _context, true)
                                .Any(legalAppCase => legalAppCase.Id == dataAccess.ItemId)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("client/{clientId}/eligible-users")]
        public async Task<IActionResult> GetClientEligibleUsers([FromServices] UserManager<IdentityUser> userManager,
            long clientId)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (admin?.LegalAppAccessKey is null || admin.LegalAppAccessKey?.ExpireDate <= DateTime.UtcNow)
                    return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();

                var currentAllowed = _context.LegalAppDataAccesses
                    .Where(x =>
                        x.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient && x.ItemId == clientId)
                    .ToList();

                var result = await GetOnlyNormalUsers(users, userManager);
                result.RemoveAll(x =>
                    !x.Role.Equals(SystemyWpConstants.Roles.User) ||
                    currentAllowed.Any(y => y.UserId == x.Id));

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}