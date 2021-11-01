using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.RestaurantApp.Dish
{
    [Route("/api/restaurant-app/menu")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class RestaurantAppDishController   : RestaurantAppApiControllerBase
    {
        public RestaurantAppDishController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        
        
        
        
        
        
    }
}