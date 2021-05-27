using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.CustomAttributes;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Repositories.Base
{
    [TransientService]
    public class RepositoryBase
    {
        protected readonly AppDbContext _context;
        protected readonly PortalLogger _logger;

        public RepositoryBase(AppDbContext context, PortalLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        protected ClaimsPrincipal CurrentClaimsPrincipal;

        protected string UserId => GetClaim(CurrentClaimsPrincipal, ClaimTypes.NameIdentifier);
        protected string UserName => GetClaim(CurrentClaimsPrincipal, ClaimTypes.Name);
        protected string UserEmail => GetClaim(CurrentClaimsPrincipal, ClaimTypes.Email);
        protected string Role => GetClaim(CurrentClaimsPrincipal, SystemyWpConstants.Claims.Role);

        protected string GetClaim(ClaimsPrincipal claimsPrincipal, string claimType) => claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected Task HandleException(Exception ex, string endpoint, string description)
        {
            return _logger.Log(LogType.Exception, endpoint, UserId, UserEmail, description, ex);
        }

        public class CheckResult
        {
            public bool AdminUser { get; set; }
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
                return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey, AdminUser = true};
            }

            //Logic for ordinary Users
            if (Role.Equals(SystemyWpConstants.Roles.Client))
            {
                //Check DataAccess Table
                var access = await _context.DataAccesses
                    .AnyAsync(x => x.UserId.Equals(UserId) &&
                                   x.RestrictedType == restrictedType &&
                                   x.ItemId == itemId);

                if (access)
                    return new CheckResult {DataAccessAllowed = true, AccessKey = user.AccessKey, AdminUser = false};
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