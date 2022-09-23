using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Controllers.MasterService
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string UserEmail => GetClaim(ClaimTypes.Email);
        protected string UserAccessKey => GetClaim(AppConstants.ClaimNames.UserAccessKey);

        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
    }
}