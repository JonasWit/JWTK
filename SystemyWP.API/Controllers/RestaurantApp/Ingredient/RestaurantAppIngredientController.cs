using SystemyWP.API.Controllers.BaseClases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.RestaurantApp.Ingredient
{
    [Route("/api/restaurant-app/menu")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class RestaurantAppIngredientController : RestaurantAppApiControllerBase
    {
        public RestaurantAppIngredientController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}