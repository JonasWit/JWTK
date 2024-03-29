using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.MasterService;
using SystemyWP.API.Data.DTOs.Gastronomy;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Data.Repositories;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Controllers.GastronomyService;

[Authorize]
[Route("[controller]")]
public class GastronomyDishesController : ApiControllerBase
{
    private readonly IOptionsMonitor<ClusterServices> _clusterServicesSettings;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyDishesController> _logger;
    private readonly IMapper _mapper;
    private readonly UrlService _urlService;
    private readonly UserRepository _userRepository;

    public GastronomyDishesController(
        IOptionsMonitor<ClusterServices> clusterServicesSettings,
        UrlService urlService,
        UserRepository userRepository,
        IMapper mapper,
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyDishesController> logger)
    {
        _clusterServicesSettings = clusterServicesSettings;
        _urlService = urlService;
        _userRepository = userRepository;
        _mapper = mapper;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }

    [HttpPost(Name = "CreateDish")]
    public async Task<IActionResult> CreateDish([FromBody] DishCreatePayload dishCreatePayload)
    {
        try
        {
            DishCreateDto dishCreateDto = _mapper.Map<DishCreateDto>(dishCreatePayload);
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            dishCreateDto.AccessKey = key;
            DishDto ingredientDto = await _gastronomyHttpClient.CreateDish(dishCreateDto);
            if (ingredientDto is null)
            {
                throw new Exception();
            }

            return CreatedAtRoute(nameof(GetDish), new { ingredientDto.Id }, ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CreateDish)} Error");
            return Problem($"{nameof(CreateDish)} Error");
        }
    }

    [HttpGet("{id:long}", Name = "GetDish")]
    public async Task<IActionResult> GetDish(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            DishServiceDto dishServiceDto = await _gastronomyHttpClient.GetDish(new ResourceAccessPass { Id = id, AccessKey = key });
            if (dishServiceDto is null)
            {
                return NotFound();
            }

            return Ok(new DishDto
            {
                Name = dishServiceDto.Name,
                Id = dishServiceDto.Id,
                Description = dishServiceDto.Description,
                Ingredients = dishServiceDto.Ingredients
                    .Select(item => _urlService.IngredientPath("GastronomyIngredients", item).ToString()).ToList(),
                Menus = dishServiceDto.Menus
                    .Select(item => _urlService.IngredientPath("GastronomyMenus", item).ToString()).ToList()
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetDish)} Error");
            return Problem($"{nameof(GetDish)} Error");
        }
    }

    [HttpGet("list", Name = "GetDishes")]
    public async Task<IActionResult> GetDishes()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            List<DishServiceDto> dishes = await _gastronomyHttpClient.GetDishes(key);
            if (dishes is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<DishDto>>(dishes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetDishes)} Error");
            return Problem($"{nameof(GetDishes)} Error");
        }
    }

    [HttpDelete("{id:long}", Name = "RemoveDish")]
    public async Task<IActionResult> RemoveDish(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            HttpStatusCode responseCode =
                await _gastronomyHttpClient.RemoveDish(new ResourceAccessPass { Id = id, AccessKey = key });
            return responseCode switch
            {
                HttpStatusCode.NoContent => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception("RemoveDish Error - Service Error"),
                _ => throw new Exception("RemoveDish Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveDish)} Error");
            return Problem($"{nameof(RemoveDish)} Error");
        }
    }

    [HttpGet("count", Name = "CountDishes")]
    public async Task<IActionResult> CountDishes()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            ElementCountDto elementCountDto = await _gastronomyHttpClient.CountDishes(key);
            if (elementCountDto is null)
            {
                return NotFound();
            }

            return Ok(elementCountDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CountDishes)} Error");
            return Problem($"{nameof(CountDishes)} Error");
        }
    }

    [HttpPut(Name = "UpdateDish")]
    public async Task<IActionResult> UpdateDish([FromBody] DishUpdatePayload dishUpdatePayload)
    {
        try
        {
            DishUpdateDto dishUpdateDto = _mapper.Map<DishUpdateDto>(dishUpdatePayload);
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            dishUpdateDto.AccessKey = key;

            HttpStatusCode responseCode = await _gastronomyHttpClient.UpdateDish(dishUpdateDto);
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
            _logger.LogError(e, $"{nameof(UpdateDish)} Error");
            return Problem($"{nameof(UpdateDish)} Error");
        }
    }

    [HttpGet("list/{cursor:int}/{take:int}", Name = "GetPaginatedDishes")]
    public async Task<IActionResult> GetPaginatedDishes(int cursor, int take)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            List<DishServiceDto> dishes = await _gastronomyHttpClient.GetPaginatedDishes(key, cursor, take);
            if (dishes is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<DishDto>>(dishes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetPaginatedDishes)} Error");
            return Problem($"{nameof(GetPaginatedDishes)} Error");
        }
    }

    [HttpPost("add-ingredient", Name = "AddDishIngredient")]
    public async Task<IActionResult> AddDishIngredient(
        [FromBody] DishIngredientUpdatePayload dishIngredientUpdatePayload)
    {
        try
        {
            DishIngredientUpdateDto dishIngredientUpdateDto = _mapper.Map<DishIngredientUpdateDto>(dishIngredientUpdatePayload);
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            dishIngredientUpdateDto.AccessKey = key;

            HttpStatusCode responseCode = await _gastronomyHttpClient.AddIngredientToDish(dishIngredientUpdateDto);
            return responseCode switch
            {
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception(
                    $"{nameof(AddDishIngredient)} Error - Service Error"),
                _ => throw new Exception($"{nameof(AddDishIngredient)} Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(AddDishIngredient)} Error");
            return Problem($"{nameof(AddDishIngredient)} Error");
        }
    }

    [HttpPost("remove-ingredient", Name = "RemoveDishIngredient")]
    public async Task<IActionResult> RemoveDishIngredient(
        [FromBody] DishIngredientUpdatePayload dishIngredientUpdatePayload)
    {
        try
        {
            DishIngredientUpdateDto dishIngredientUpdateDto = _mapper.Map<DishIngredientUpdateDto>(dishIngredientUpdatePayload);
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            dishIngredientUpdateDto.AccessKey = key;

            HttpStatusCode responseCode = await _gastronomyHttpClient.RemoveIngredientFromDish(dishIngredientUpdateDto);
            return responseCode switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception(
                    $"{nameof(RemoveDishIngredient)} Error - Service Error"),
                _ => throw new Exception($"{nameof(RemoveDishIngredient)} Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveDishIngredient)} Error");
            return Problem($"{nameof(RemoveDishIngredient)} Error");
        }
    }
}