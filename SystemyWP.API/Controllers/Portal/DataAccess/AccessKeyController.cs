using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.Portal.DataAccess
{
    [Route("/api/portal-admin/key-admin")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class AccessKeyController : ApiController
    {
        public AccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpPost("user/revoke/access-key/{userId}")]
        public async Task<IActionResult> RevokeDataAccessKey(string userId, [FromServices] UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                var userProfile = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (user is null || userProfile is null) return BadRequest(SystemyWpConstants.ResponseMessages.DataNotFound);

                var assignedLegalAppKey = _context.LegalAppAccessKeys
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));
                
                var assignedMedicalAppKey = _context.MedicalAccessKeys
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Users.Any(y => y.Id.Equals(user.Id)));

                if (assignedLegalAppKey is not null || assignedMedicalAppKey is not null)
                {
                    assignedLegalAppKey?.Users.RemoveAll(x => x.Id.Equals(user.Id));
                    assignedMedicalAppKey?.Users.RemoveAll(x => x.Id.Equals(user.Id));
                    
                    _context.RemoveRange(_context.LegalAppDataAccesses.Where(x => x.UserId.Equals(userId)));
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