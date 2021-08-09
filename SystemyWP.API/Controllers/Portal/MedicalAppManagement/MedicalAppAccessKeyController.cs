using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.Portal.MedicalAppManagement
{
    [Route("/api/portal-admin/key-admin/medical-app")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class MedicalAppAccessKeyController : ApiController
    {
        public MedicalAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpPost("revoke/{userId}")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> RevokeMedicalAppDataAccessKey(string userId, [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);
                
                var assignedMedicalAppKey = _context.MedicalAccessKeys
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                if (assignedMedicalAppKey is not null)
                {
                    assignedMedicalAppKey.Users.RemoveAll(x => x.Id.Equals(user.Id));
                    _context.RemoveRange(_context.MedicalAppDataAccesses.Where(x => x.UserId.Equals(userId)));
                    
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