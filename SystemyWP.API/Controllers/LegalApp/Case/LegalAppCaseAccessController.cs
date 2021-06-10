using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Case
{
    [Route("/api/legal-app-case-access")]
    [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
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
                    .GetAllowedCase(UserId, Role, caseId, _context, true)
                    .FirstOrDefault();

                if (lappCase is null) return BadRequest();

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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("case/{caseId}/grant-access/{userId}")]
        public async Task<IActionResult> GrantAccess([FromServices] UserManager<IdentityUser> userManager,
            long caseId,
            string userId)
        {
            try
            {
                var lappCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context, true)
                    .FirstOrDefault();
                if (lappCase is null) return BadRequest();

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);

                // Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await CreateUsersOutput(users, userManager);
                if (!result.Any(x => x.Id.Equals(userId))) return StatusCode(StatusCodes.Status403Forbidden);

                var currentAccesses = _context.DataAccesses
                    .Where(x => x.RestrictedType == RestrictedType.LegalAppCase && x.UserId.Equals(userId))
                    .ToList();
                if (currentAccesses.Any(x => x.ItemId == caseId)) return BadRequest();

                _context.DataAccesses.Add(new DataAccess
                {
                    UserId = userId,
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("case/{caseId}/revoke-access/{userId}")]
        public async Task<IActionResult> RevokeAccess([FromServices] UserManager<IdentityUser> userManager,
            long caseId,
            string userId)
        {
            try
            {
                var lappCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context, true)
                    .FirstOrDefault();
                if (lappCase is null) return BadRequest();

                var admin = _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);

                // Eligible users
                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();
                var result = await CreateUsersOutput(users, userManager);
                if (!result.Any(x => x.Id.Equals(userId))) return StatusCode(StatusCodes.Status403Forbidden);

                var currentAccess = _context.DataAccesses
                    .FirstOrDefault(x => x.RestrictedType == RestrictedType.LegalAppCase && x.UserId.Equals(userId));
                if (currentAccess is null) return BadRequest();

                _context.Remove(currentAccess);

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                if (admin?.LegalAppAccessKey is null) return BadRequest();

                var users = _context.Users
                    .Where(x => x.LegalAppAccessKey.Id == admin.LegalAppAccessKey.Id)
                    .ToList();

                var currentAllowed = _context.DataAccesses
                    .Where(x => 
                        x.RestrictedType == RestrictedType.LegalAppCase && x.ItemId == caseId)
                    .ToList();

                var result = await CreateUsersOutput(users, userManager);
                result.RemoveAll(x =>
                    !x.Role.Equals(SystemyWpConstants.Roles.Client) ||
                    currentAllowed.Any(y => y.UserId == x.Id));

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}