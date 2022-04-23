using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.Controllers.MasterService;
using SystemyWP.API.DTOs.Gastronomy;
using SystemyWP.API.DTOs.General;
using SystemyWP.API.Repositories;
using SystemyWP.API.Services.HttpClients;

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
            if (key is null) return BadRequest();
    
            ingredientCreateDto.AccessKey = key;
            var ingredientDto = await _gastronomyHttpClient.CreateIngredient(ingredientCreateDto);
            if (ingredientDto is null) throw new Exception();
    
            return CreatedAtRoute(nameof(GetIngredient), new {ingredientDto.Id}, ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.CreateIngredient);
            return Problem(ServicesConstants.GastronomyErrors.CreateIngredient);
        }
    }
    
    [HttpGet("{id:long}", Name = "GetIngredient")]
    public async Task<IActionResult> GetIngredient(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
    
            var ingredientDto =
                await _gastronomyHttpClient.GetIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            if (ingredientDto is null) return NotFound();
    
            return Ok(ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.GetIngredient);
            return Problem(ServicesConstants.GastronomyErrors.GetIngredient);
        }
    }
    
    [HttpGet("list", Name = "GetIngredients")]
    public async Task<IActionResult> GetIngredients()
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
    
            var ingredients = await _gastronomyHttpClient.GetIngredients(key);
            if (ingredients is null) return NotFound();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.GetIngredients);
            return Problem(ServicesConstants.GastronomyErrors.GetIngredients);
        }
    }
    
    [HttpGet("list/{cursor:int}/{take:int}", Name = "GetPaginatedIngredients")]
    public async Task<IActionResult> GetPaginatedIngredients(int cursor, int take)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
    
            var ingredients = await _gastronomyHttpClient.GetPaginatedIngredients(key, cursor, take);
            if (ingredients is null) return NotFound();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.GetIngredients);
            return Problem(ServicesConstants.GastronomyErrors.GetIngredients);
        }
    }
    
    [HttpDelete(Name = "RemoveIngredient")]
    public async Task<IActionResult> RemoveIngredient(long id)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
    
            var responseCode = await _gastronomyHttpClient.RemoveIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            return responseCode switch
            {
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception(ServicesConstants.GastronomyErrors
                    .InternalErrorFromService),
                _ => throw new Exception(ServicesConstants.GastronomyErrors.InternalUnsupportedStatusCode)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.RemoveIngredient);
            return Problem(ServicesConstants.GastronomyErrors.RemoveIngredient);
        }
    }
    
    [HttpPut(Name = "UpdateIngredient")]
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
            ingredientDto.AccessKey = key;
            
            var responseCode = await _gastronomyHttpClient.UpdateIngredient(ingredientDto);
            return responseCode switch
            {
                HttpStatusCode.OK => Ok(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.InternalServerError => throw new Exception(ServicesConstants.GastronomyErrors
                    .InternalErrorFromService),
                _ => throw new Exception(ServicesConstants.GastronomyErrors.InternalUnsupportedStatusCode)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, ServicesConstants.GastronomyErrors.UpdateIngredient);
            return Problem(ServicesConstants.GastronomyErrors.UpdateIngredient);
        }
    }
    
    
    
    
    
    
}