using Microsoft.AspNetCore.Authorization;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.BaseClases
{
    [Authorize]
    [Authorize(SystemyWpConstants.Policies.RestaurantAppAccess)]
    public class RestaurantAppApiControllerBase : ApiController
    {
        public RestaurantAppApiControllerBase(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}