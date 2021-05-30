using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.BaseClases
{
    [Authorize]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppApiController : ApiController
    {
        public LegalAppApiController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        public class CheckResult
        {
            public bool AdminUser { get; set; }
            public bool DataAccessAllowed { get; set; }
            public LegalAppAccessKey LegalAppAccessKey { get; set; }
        }

        protected async Task<CheckResult> CheckAccess(RestrictedType restrictedType, long itemId)
        {
            //Get Users Data Key
            var user = await _context.Users
                .Include(x => x.LegalAppAccessKey)
                .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user?.LegalAppAccessKey is null) return new CheckResult {DataAccessAllowed = false};

            //Logic for Admins
            if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
            {
                return new CheckResult {DataAccessAllowed = true, LegalAppAccessKey = user.LegalAppAccessKey, AdminUser = true};
            }

            //Logic for ordinary Users
            if (Role.Equals(SystemyWpConstants.Roles.Client))
            {
                //Check DataAccess Table
                var access = await _context.DataAccesses
                    .AnyAsync(x => 
                        x.UserId.Equals(UserId) &&
                        x.RestrictedType == restrictedType &&
                        x.ItemId == itemId);

                if (access)
                {
                    return new CheckResult {DataAccessAllowed = true, LegalAppAccessKey = user.LegalAppAccessKey, AdminUser = false};
                }
            }

            return new CheckResult {DataAccessAllowed = false, LegalAppAccessKey = user.LegalAppAccessKey};
        }

        protected async Task<CheckResult> CheckAccess()
        {
            //Get Users Data Key
            var user = await _context.Users
                .Include(x => x.LegalAppAccessKey)
                .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
            if (user?.LegalAppAccessKey is null) return new CheckResult {DataAccessAllowed = false};

            return new CheckResult {DataAccessAllowed = true, LegalAppAccessKey = user.LegalAppAccessKey};
        }
    }
}