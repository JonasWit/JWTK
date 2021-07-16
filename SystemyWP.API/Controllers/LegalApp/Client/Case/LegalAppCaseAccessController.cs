using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.Admin;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client.Case
{
    [Route("/api/legal-app-case-access")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class LegalAppCaseAccessController : LegalAppApiController
    {
        public LegalAppCaseAccessController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("case/{caseId}/allowed-users")]
        public async Task<IActionResult> GetClientAllowedUsers(long caseId)
        {
            try
            {
                var lappCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .FirstOrDefault();

                if (lappCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var allowedUsers = _context.DataAccesses
                    .Where(x => x.ItemId == lappCase.Id && x.RestrictedType == RestrictedType.LegalAppCase)
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

        [HttpPost("case/{caseId}/grant-access")]
        public async Task<IActionResult> GrantAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            long caseId)
        {
            try
            {
                var lappCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .FirstOrDefault();
                if (lappCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (admin is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                    
                // Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await GetOnlyNormalUsers(users, userManager);
                if (!result.Any(x => x.Id.Equals(form.UserId))) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var currentCasesAccesses = _context.DataAccesses
                    .Where(x =>
                        x.RestrictedType == RestrictedType.LegalAppCase &&
                        x.UserId.Equals(form.UserId) &&
                        x.ItemId == caseId)
                    .FirstOrDefault();
                if (currentCasesAccesses is not null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                // Add access to client if there is none already
                var currentClientsAccesses = _context.DataAccesses
                    .Where(x =>
                        x.RestrictedType == RestrictedType.LegalAppClient &&
                        x.UserId.Equals(form.UserId) &&
                        x.ItemId == lappCase.LegalAppClientId)
                    .FirstOrDefault();
                if (currentClientsAccesses is null)
                {
                    _context.DataAccesses.Add(new DataAccess
                    {
                        LegalAppAccessKey = admin.LegalAppAccessKey,
                        UserId = form.UserId,
                        CreatedBy = UserEmail,
                        ItemId = lappCase.LegalAppClientId,
                        RestrictedType = RestrictedType.LegalAppClient
                    });
                }

                _context.DataAccesses.Add(new DataAccess
                {
                    LegalAppAccessKey = admin.LegalAppAccessKey,
                    UserId = form.UserId,
                    CreatedBy = UserEmail,
                    ItemId = caseId,
                    RestrictedType = RestrictedType.LegalAppCase
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

        [HttpPost("case/{caseId}/revoke-access")]
        public async Task<IActionResult> RevokeAccess(
            [FromBody] UserIdForm form,
            [FromServices] UserManager<IdentityUser> userManager,
            long caseId)
        {
            try
            {
                var lappCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .FirstOrDefault();
                if (lappCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);

                // Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await GetOnlyNormalUsers(users, userManager);
                if (!result.Any(x => x.Id.Equals(form.UserId))) return StatusCode(StatusCodes.Status403Forbidden);

                var currentAccess = _context.DataAccesses
                    .FirstOrDefault(x => x.RestrictedType == RestrictedType.LegalAppCase && x.UserId.Equals(form.UserId));
                if (currentAccess is null) return BadRequest();

                _context.Remove(currentAccess);

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("case/{caseId}/eligible-users")]
        public async Task<IActionResult> GetClientEligibleUsers([FromServices] UserManager<IdentityUser> userManager,
            long caseId)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (admin?.LegalAppAccessKey is null || admin.LegalAppAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();

                var currentAllowed = _context.DataAccesses
                    .Where(x =>
                        x.RestrictedType == RestrictedType.LegalAppCase && x.ItemId == caseId)
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