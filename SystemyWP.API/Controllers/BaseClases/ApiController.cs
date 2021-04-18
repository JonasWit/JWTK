using System.Linq;
using System.Security.Claims;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly PortalLogger _portalLogger;
        protected readonly AppDbContext _context;

        public ApiController(PortalLogger portalLogger, AppDbContext context)
        {
            _portalLogger = portalLogger;
            _context = context;
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