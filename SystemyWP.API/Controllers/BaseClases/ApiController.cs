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

        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string Username => GetClaim(ClaimTypes.Name);
        protected string UserEmail => GetClaim(ClaimTypes.Email);
        protected string Role => GetClaim(SystemyWPConstants.Claims.Role);

        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected bool LegalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWPConstants.Apps.LegalApp));
    }
}