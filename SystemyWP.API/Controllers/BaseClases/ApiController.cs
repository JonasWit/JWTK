using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly PortalLogger _portalLogger;

        public ApiController(PortalLogger portalLogger)
        {
            _portalLogger = portalLogger;
        }

        protected async Task<User> GetUserProfile([FromServices] AppDbContext context) => await context.Users
            .Where(x => x.Id.Equals(UserId))
            .Include(x => x.AccessKey)
            .FirstOrDefaultAsync();

        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string Username => GetClaim(ClaimTypes.Name);
        protected string Role => GetClaim(SystemyWPConstants.Claims.Role);

        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected bool LegalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWPConstants.Apps.LegalApp));
    }
}