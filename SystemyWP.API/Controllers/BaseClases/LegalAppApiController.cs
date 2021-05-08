using System.Linq;
using System.Threading.Tasks;
using Systemywp.Api.Services.Logging;
using Systemywp.Data;
using Systemywp.Data.DataAccessModifiers;
using Systemywp.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Systemywp.Api.Controllers.BaseClases
{
    [Authorize]
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

            //Logic for Admins
            if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
            {
                return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey};
            }
            
            //Logic for ordinary Users
            if (Role.Equals(SystemyWpConstants.Roles.Client))
            {
                //Check DataAccess Table
                var access = await _context.DataAccesses
                    .AnyAsync(x => x.UserId.Equals(UserId) &&
                                   x.RestrictedType == restrictedType &&
                                   x.ItemId == itemId);

                if (access) return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey};
            }         
            
            return new CheckResult {DataAccessAllowed = false, AccessKey = user.AccessKey};
        }
        protected async Task<CheckResult> CheckAccess()
        {
            //Get Users Data Key
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user?.AccessKey is null) return new CheckResult {DataAccessAllowed = false};
            
            return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey};
        }
    }
}