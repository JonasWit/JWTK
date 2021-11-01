using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.CustomExtensions.RestaurantAppExtensions.Menu;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Projections.RestaurantApp.Menu;

namespace SystemyWP.API.Controllers.RestaurantApp.Menu
{
    [Route("/api/restaurant-app/menu")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class RestaurantAppMenuController : RestaurantAppApiControllerBase
    {
        public RestaurantAppMenuController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("client/{menuId}")]
        public async Task<IActionResult> GetMenu(int menuId)
        {
            try
            {
                var result = _context.RestaurantAppMenus
                    .GetAllowedMenu(UserId, Role, menuId, _context, true)
                    .Select(RestaurantAppMenuProjections.FlatProjection)
                    .FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        
        
    }
}