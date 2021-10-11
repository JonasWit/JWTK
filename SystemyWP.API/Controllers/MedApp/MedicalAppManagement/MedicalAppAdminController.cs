using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.MedApp.MedicalAppManagement
{
    [Route("/api/medical-app-admin/general")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class MedicalAppAdminController : ApiController
    {
        public MedicalAppAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
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
                    .Include(x => x.MedicalAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (adminUser is null) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectBehaviour);
                if (adminUser.MedicalAccessKey is null) return Ok(result);

                //Get related users with the same data access key
                var relatedUsers = _context.Users
                    .Include(x => x.MedicalAccessKey)
                    .Where(x => x.MedicalAccessKey.Id == adminUser.MedicalAccessKey.Id)
                    .Include(x => x.MedicalAppDataAccesses)
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
    }
}