using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.API.ViewModels.General;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        protected async Task<bool> ValidateLegalAppKey()
        {
            try
            {
                var user = _context.Users.Include(x => x.LegalAccessKey).FirstOrDefault(x => x.Id.Equals(UserId));
                if (user?.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return true;
                return false;
            }
            catch (Exception e)
            {
                await HandleException(e);
                return false;
            }
        }

        protected async Task<List<UserBasic>> GetOnlyNormalUsers(List<User> users, UserManager<IdentityUser> userManager)
        {
            var output = new List<UserBasic>();
            foreach (var user in users)
            {
                var userClaims = await userManager.GetClaimsAsync(await userManager.FindByIdAsync(user.Id)) as List<Claim> ?? new List<Claim>();
                var role = userClaims.FirstOrDefault(x => x.Type.Equals(SystemyWpConstants.Claims.Role))?.Value;
                output.Add(new UserBasic
                {
                    Email = user.Email,
                    Id = user.Id,
                    Username = user.Username,
                    Role = role
                });
            }

            return output;
        }
    }
}