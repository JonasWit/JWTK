using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-statistics")]
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
            await _portalLogger.Log(LogType.Access, $"Related users requested", UserId, Username);
            var result = new List<object>();
            
            try
            {
                var adminUser = context.Users.FirstOrDefault(x => x.Id.Equals(UserId));
                var accessKey = context.AccessKeys
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(UserId)));

                var relatedUsers = context.Users
                    .Include(x => x.DataAccess)
                    // .AsEnumerable()
                    // .Where(x => 
                    //     accessKey != null && accessKey.Users.Any(y => y.Id.Equals(x.Id)))
                    .ToList();

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
            }
            catch (Exception ex)
            {
                await _portalLogger.Log(LogType.Exception, ex.Message, ex.StackTrace, UserId, Username);
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("related-data")]
        public async Task<ActionResult<IEnumerable<object>>> GetClientsAndCasesForAccess(
            [FromServices] AppDbContext context)
        {
            try
            {
        
            }
            catch (Exception ex)
            {
                await _portalLogger.Log(LogType.Exception, ex.Message, ex.StackTrace, UserId, Username);
                return BadRequest();
            }


            throw new NotImplementedException();
        }
    }
}