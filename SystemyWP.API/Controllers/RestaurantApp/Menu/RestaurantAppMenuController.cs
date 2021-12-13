using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.CustomExtensions.RestaurantAppExtensions.Menu;

namespace SystemyWP.API.Controllers.RestaurantApp.Menu
{
    [Route("/api/restaurant-app/menu")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class RestaurantAppMenuController : RestaurantAppApiControllerBase
    {
        private readonly IMapper _mapper;

        public RestaurantAppMenuController(IMapper mapper, PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
            _mapper = mapper;
        }
        
        [HttpGet("client/{menuId}")]
        public async Task<IActionResult> GetMenu(int menuId)
        {
            try
            {
                var result = _context.RestaurantAppMenus
                    .GetAllowedMenu(UserId, Role, menuId, _context, true)
                    .FirstOrDefault();
                
                
                
                
                
                

                if (result is null) return NotFound();

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