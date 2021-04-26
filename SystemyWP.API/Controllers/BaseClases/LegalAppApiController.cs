using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.BaseClases
{
    public class LegalAppApiController : ApiController
    {
        public LegalAppApiController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        public class CheckResult
        {
            public bool DataAccessAllowed { get; set; }
            public AccessKey AccessKey { get; set; }
        }

        protected async Task<CheckResult> CheckAccess(RestrictedType restrictedType, long itemId)
        {
            //Get Users Data Key
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user?.AccessKey is null) return new CheckResult {DataAccessAllowed = false};

            //Check DataAccess Table
            var access = await _context.DataAccesses
                .FirstOrDefaultAsync(x => x.UserId.Equals(UserId) &&
                                          x.RestrictedType == restrictedType &&
                                          x.ItemId == itemId);
            if (access is null) return new CheckResult {DataAccessAllowed = false, AccessKey = user.AccessKey};
            
            return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey};
        }
    }
}