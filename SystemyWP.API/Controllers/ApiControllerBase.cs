using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string UserEmail => GetClaim(ClaimTypes.Email);

        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
    }
}