using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Controllers.MasterService;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Data.Repositories;
using SystemyWP.API.Services.HttpServices;

//todo: do this controller

namespace SystemyWP.API.Controllers.GastronomyService;

[Authorize]
[Route("[controller]")]
public class GastronomyMenusController : ApiControllerBase
{
    private readonly UrlService _urlService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyMenusController> _logger;

    public GastronomyMenusController(
        UrlService urlService,
        IUserRepository userRepository,
        IMapper mapper,
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyMenusController> logger)
    {
        _urlService = urlService;
        _userRepository = userRepository;
        _mapper = mapper;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }
    
    
    [HttpPost(Name = "CreateMenu")]
    public async Task<IActionResult> CreateMenu([FromBody] MenuCreateDto menuCreateDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();

            menuCreateDto.AccessKey = key;
            var menuDto = await _gastronomyHttpClient.CreateMenu(menuCreateDto);
            if (menuDto is null) throw new Exception();

            return CreatedAtRoute(nameof(GetMenu), new {menuDto.Id}, menuDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CreateMenu)} Error");
            return Problem($"{nameof(CreateMenu)} Error");
        }
    }
    
    [HttpGet("{id:long}", Name = "GetMenu")]
    public async Task<IActionResult> GetMenu(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();

            var menuServiceDto = await _gastronomyHttpClient.GetMenu(new ResourceAccessPass {Id = id, AccessKey = key});
            if (menuServiceDto is null) return NotFound();
            
            return Ok(new MenuDto
            {
                Name = menuServiceDto.Name,
                Id = menuServiceDto.Id,
                Description = menuServiceDto.Description,
                Dishes = menuServiceDto.Ingredients.Select(item => _urlService.DishPath("GastronomyDishes", item).ToString()).ToList(),
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetMenu)} Error");
            return Problem($"{nameof(GetMenu)} Error");
        }
    }
    
    [HttpDelete("{id:long}", Name = "RemoveMenu")]
    public async Task<IActionResult> RemoveMenu(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var responseCode = await _gastronomyHttpClient.RemoveMenu(new ResourceAccessPass {Id = id, AccessKey = key});
            return responseCode switch
            {
                HttpStatusCode.NoContent => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception("RemoveMenu Error - Service Error"),
                _ => throw new Exception("RemoveMenu Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveMenu)} Error");
            return Problem($"{nameof(RemoveMenu)} Error");
        }
    }
    
    [HttpGet("list", Name = "GetMenus")]
    public async Task<IActionResult> GetMenus()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var response = await _gastronomyHttpClient.GetMenus(key);
            if (response is null) return NotFound();
            
            return Ok(response.Select(serviceDto => new MenuDto
            {
                Name = serviceDto.Name,
                Id = serviceDto.Id,
                Description = serviceDto.Description,
                Dishes = serviceDto.Dishes.Select(item => _urlService.DishPath("GastronomyDishes", item).ToString()).ToList(),
            }).ToList());
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetMenus)} Error");
            return Problem($"{nameof(GetMenus)} Error");
        }
    }
    
    [HttpGet("count", Name = "CountMenus")]
    public async Task<IActionResult> CountMenus()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var elementCountDto = await _gastronomyHttpClient.CountMenus(key);
            if (elementCountDto is null) return NotFound();
            return Ok(elementCountDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CountMenus)} Error");
            return Problem($"{nameof(CountMenus)} Error");
        }
    }
    
    [HttpPut(Name = "UpdateMenu")]
    public async Task<IActionResult> UpdateMenu([FromBody] MenuUpdateDto menuUpdateDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
            menuUpdateDto.AccessKey = key;
            
            var responseCode = await _gastronomyHttpClient.UpdateMenu(menuUpdateDto);
            return responseCode switch
            {
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception("UpdateDish Error - Service Error"),
                _ => throw new Exception("UpdateDish Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(UpdateMenu)} Error");
            return Problem($"{nameof(UpdateMenu)} Error");
        }
    }
    
    [HttpGet("list/{cursor:int}/{take:int}", Name = "GetPaginatedMenus")]
    public async Task<IActionResult> GetPaginatedMenus(int cursor, int take)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var menus = await _gastronomyHttpClient.GetPaginatedMenus(key, cursor, take);
            if (menus is null) return NotFound();
            
            return Ok(menus.Select(menuServiceDto => new MenuDto
            {
                Name = menuServiceDto.Name,
                Id = menuServiceDto.Id,
                Description = menuServiceDto.Description,
                Dishes = menuServiceDto.Dishes.Select(item => _urlService.IngredientPath("GastronomyDishes", item).ToString()).ToList(),
            }).ToList());  
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetPaginatedMenus)} Error");
            return Problem($"{nameof(GetPaginatedMenus)} Error");
        }
    }
    
    [HttpPost("add-dish", Name = "AddMenuDish")]
    public async Task<IActionResult> AddMenuDish([FromBody] MenuDishUpdateDto menuDishUpdateDto)
    {
        try
        {
            
            
            
            throw new NotImplementedException();     
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(AddMenuDish)} Error");
            return Problem($"{nameof(AddMenuDish)} Error");
        }
    }
    
    
    [HttpPost("remove-dish", Name = "RemoveMenuDish")]
    public async Task<IActionResult> RemoveMenuDish([FromBody] MenuDishUpdateDto menuDishUpdateDto)
    {
        try
        {
            
            
            
            
            throw new NotImplementedException();    
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveMenuDish)} Error");
            return Problem($"{nameof(RemoveMenuDish)} Error");
        }
    }  
    
    
    
    
    
    
}