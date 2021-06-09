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

namespace SystemyWP.API.Controllers.BaseClases
{
    [Authorize]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppApiController : ApiController
    {
        public LegalAppApiController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        protected async Task<List<UserBasic>> CreateUsersOutput(List<User> users, UserManager<IdentityUser> userManager)
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