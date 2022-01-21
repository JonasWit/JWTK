using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult ServerError => StatusCode(StatusCodes.Status500InternalServerError, "Error occured contact admin");

        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string UserEmail => GetClaim(ClaimTypes.Email);
        protected string Role => GetClaim(SystemyWpConstants.Claims.Role);
        
        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
    }
}