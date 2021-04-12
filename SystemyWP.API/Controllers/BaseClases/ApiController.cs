using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public class CheckResult
        {
            public bool AccessData { get; set; }
            public AccessKey AccessKey { get; set; }
        }

        protected async Task<CheckResult> CheckAccess(RestrictedType restrictedType, long itemId)
        {
            //Get Users Data Key
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user?.AccessKey is null) return new CheckResult {AccessData = false};

            //Check DataAccess Table
            var access = await _context.DataAccesses
                .FirstOrDefaultAsync(x => x.UserId.Equals(UserId) &&
                                     x.RestrictedType == restrictedType &&
                                     x.ItemId == itemId);
            if (access is null) return new CheckResult {AccessData = false, AccessKey = user.AccessKey};
            
            return new CheckResult {AccessData = false, AccessKey = user.AccessKey};
        }

        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected bool LegalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWPConstants.Apps.LegalApp));
    }

    public enum CheckActionResult
    {
        
        
        
    }
}