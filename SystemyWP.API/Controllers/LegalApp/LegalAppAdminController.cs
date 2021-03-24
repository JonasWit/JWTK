using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-admin")]
    [Authorize(SystemyWPConstants.Policies.ClientAdmin)]
    public class LegalAppAdminController : ApiController
    {
        public LegalAppAdminController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        [HttpGet("related-users")]
        public async Task<ActionResult<IEnumerable<object>>> GetRelatedUsers(
            [FromServices] AppDbContext context,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            await _portalLogger.Log(LogType.Access, $"Related users data requested", UserId, Username);
            var result = new List<object>();

            try
            {
                //Get current admin who made request
                var adminUser = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                if (adminUser is null) throw new Exception("Error!");

                //Get related users with the same data access key
                var relatedUsers = context.Users
                    .Include(x => x.AccessKey)
                    .Where(x => x.AccessKey.Name.Equals(adminUser.AccessKey.Name))
                    .Include(x => x.DataAccess)
                    .ToList();

                if (relatedUsers.Count == 0) return Ok(result);

                foreach (var relatedUser in relatedUsers)
                {
                    var userRecord = await userManager.FindByIdAsync(relatedUser.Id);
                    var role = (await userManager.GetClaimsAsync(userRecord) as List<Claim>)
                        .FirstOrDefault(x => x.Type.Equals(SystemyWPConstants.Claims.Role))?
                        .Value;
                    result.Add(UserProjections
                        .RelatedUserProjection(userRecord.UserName, userRecord.Email, role)
                        .Compile()
                        .Invoke(relatedUser));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                await _portalLogger.Log(LogType.Exception, ex.Message, ex.StackTrace, UserId, Username);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-legal-app-data-access")]
        public async Task<IActionResult> UpdateLegalAppDataAccess(
            [FromBody] LegalAppUpdateUserAccessForm form,
            [FromServices] AppDbContext context)
        {
            try
            {
                var requester = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                var user = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));

                if (requester is null || 
                    user is null || 
                    requester.AccessKey is null || 
                    user.AccessKey is null)
                {
                    return BadRequest();
                }

                if (!requester.AccessKey.Name.Equals(user.AccessKey.Name))
                {
                    await _portalLogger.Log(
                        LogType.AccessViolation, 
                        $"User {Username} wanted to change access to data with but Access Keys did not match!",
                        UserId, 
                        UserEmail);
                    return BadRequest();
                }
                
                context.DataAccesses.RemoveRange(
                    context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                foreach (var allowedClient in form.AllowedClients)
                {
                    context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppClient,
                        ItemId = allowedClient,
                        UserId = user.Id
                    });
                }
                
                foreach (var allowedCase in form.AllowedCases)
                {
                    context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppCase,
                        ItemId = allowedCase,
                        UserId = user.Id,
                        CreatedBy = UserId,
                        UpdatedBy = UserId
                    });
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _portalLogger.Log(LogType.Exception, ex.Message, ex.StackTrace, UserId, Username);
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}