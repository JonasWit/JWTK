using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.Controllers.MasterService;
using SystemyWP.API.Data.DTOs.Gastronomy;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Data.Repositories;
using SystemyWP.API.Services.HttpServices;

namespace SystemyWP.API.Controllers.GastronomyService;

[Authorize]
[Route("[controller]")]
public class GastronomyIngredientsController : ApiControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyIngredientsController> _logger;

    public GastronomyIngredientsController(
        IUserRepository userRepository,
        IMapper mapper,
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyIngredientsController> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }

    [HttpPost(Name = "CreateIngredient")]
    public async Task<IActionResult> CreateIngredient([FromBody] IngredientCreateDto ingredientCreateDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            ingredientCreateDto.AccessKey = key;
            var ingredientDto = await _gastronomyHttpClient.CreateIngredient(ingredientCreateDto);
            if (ingredientDto is null) throw new Exception();
    
            return CreatedAtRoute(nameof(GetIngredient), new {ingredientDto.Id}, ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CreateIngredient)} Error");
            return Problem($"{nameof(CreateIngredient)} Error");
        }
    }
    
    [HttpGet("{id:long}", Name = "GetIngredient")]
    public async Task<IActionResult> GetIngredient(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var ingredientDto = await _gastronomyHttpClient.GetIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            if (ingredientDto is null) return NotFound();
    
            return Ok(ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetIngredient)} Error");
            return Problem($"{nameof(GetIngredient)} Error");
        }
    }
    
    [HttpGet("list", Name = "GetIngredients")]
    public async Task<IActionResult> GetIngredients()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var ingredients = await _gastronomyHttpClient.GetIngredients(key);
            if (ingredients is null) return NotFound();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetIngredients)} Error");
            return Problem($"{nameof(GetIngredients)} Error");
        }
    }
    
    [HttpGet("count", Name = "CountIngredients")]
    public async Task<IActionResult> CountIngredients()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var ingredients = await _gastronomyHttpClient.CountIngredients(key);
            if (ingredients is null) return NotFound();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CountIngredients)} Error");
            return Problem($"{nameof(CountIngredients)} Error");
        }
    }
    
    [HttpGet("list/{cursor:int}/{take:int}", Name = "GetPaginatedIngredients")]
    public async Task<IActionResult> GetPaginatedIngredients(int cursor, int take)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var ingredients = await _gastronomyHttpClient.GetPaginatedIngredients(key, cursor, take);
            if (ingredients is null) return NotFound();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetPaginatedIngredients)} Error");
            return Problem($"{nameof(GetPaginatedIngredients)} Error");
        }
    }
    
    [HttpDelete("{id:long}", Name = "RemoveIngredient")]
    public async Task<IActionResult> RemoveIngredient(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
    
            var responseCode = await _gastronomyHttpClient.RemoveIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            return responseCode switch
            {
                HttpStatusCode.NoContent => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception("RemoveIngredient Error - Service Error"),
                _ => throw new Exception("RemoveIngredient Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveIngredient)} Error");
            return Problem($"{nameof(RemoveIngredient)} Error");
        }
    }
    
    [HttpPut(Name = "UpdateIngredient")]
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientUpdateDto ingredientUpdateDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (string.IsNullOrEmpty(key)) return BadRequest();
            ingredientUpdateDto.AccessKey = key;
            
            var responseCode = await _gastronomyHttpClient.UpdateIngredient(ingredientUpdateDto);
            return responseCode switch
            {
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception("RemoveIngredient Error - Service Error"),
                _ => throw new Exception("RemoveIngredient Error - Unsupported Status Code")
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(UpdateIngredient)} Error");
            return Problem($"{nameof(UpdateIngredient)} Error");
        }
    }
    
    
    
    
    
    
}