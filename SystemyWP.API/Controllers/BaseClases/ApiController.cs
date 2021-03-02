using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string Username => GetClaim(ClaimTypes.Name);
        protected string Role => GetClaim(SystemyWPConstants.Claims.Role);
        
        protected string DataAccessKey => GetClaim(SystemyWPConstants.Claims.DataAccessKey);
        
        protected string GetClaim(string claimType) => User.Claims.FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected List<string> AllowedApps => User.Claims.Where(x => x.Type.Equals(SystemyWPConstants.Claims.AppAccess)).Select(x => x.Value).ToList();

    }
}