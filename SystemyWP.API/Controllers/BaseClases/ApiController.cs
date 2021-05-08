using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
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
        protected string Role => GetClaim(SystemyWpConstants.Claims.Role);

        protected Task LogException(Exception ex)
        {
            return _portalLogger.Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, "Exception In Controller.");        
        }
        
        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected bool LegalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWpConstants.Apps.LegalApp));
    }
}