using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-client-access")]
    [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
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
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest();

                var allowedUsers = _context.DataAccesses
                    .Where(x => x.ItemId == clientId && x.RestrictedType == RestrictedType.LegalAppClient)
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

        [HttpPost("client/{clientId}/grant-access/{userId}")]
        public async Task<IActionResult> GrantAccess([FromServices] UserManager<IdentityUser> userManager,
            long clientId,
            string userId)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest();

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
                    .Where(x => x.RestrictedType == RestrictedType.LegalAppClient && x.UserId.Equals(userId))
                    .ToList();
                if (currentAccesses.Any(x => x.ItemId == clientId)) return BadRequest();

                _context.DataAccesses.Add(new DataAccess
                {
                    UserId = userId,
                    CreatedBy = UserEmail,
                    ItemId = clientId,
                    RestrictedType = RestrictedType.LegalAppClient
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

        [HttpPost("client/{clientId}/revoke-access/{userId}")]
        public async Task<IActionResult> RevokeAccess([FromServices] UserManager<IdentityUser> userManager,
            long clientId,
            string userId)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest();

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
                    .FirstOrDefault(x => x.RestrictedType == RestrictedType.LegalAppClient && x.UserId.Equals(userId));
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

        [HttpGet("client/{clientId}/eligible-users")]
        public async Task<IActionResult> GetClientEligibleUsers([FromServices] UserManager<IdentityUser> userManager,
            long clientId)
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
                        x.RestrictedType == RestrictedType.LegalAppClient && x.ItemId == clientId)
                    .ToList();

                var result = await CreateUsersOutput(users, userManager);
                result.RemoveAll(x =>
                    !x.Role.Equals(SystemyWpConstants.Roles.Client) &&
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