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
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
    {
        try
        {
            var key = _userRepository.GetUserAccessKey(UserId);
            if (key is null) return BadRequest();
    
            createIngredientDto.AccessKey = key;
            var ingredientDto = await _gastronomyHttpClient.CreateIngredient(createIngredientDto);
            if (ingredientDto is null) throw new Exception();
    
            return CreatedAtRoute(nameof(GetIngredient), new {ingredientDto.Id}, ingredientDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, UrlService.GastronomyErrors.CreateIngredient);
            return Problem(UrlService.GastronomyErrors.CreateIngredient);
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
            _logger.LogError(e, UrlService.GastronomyErrors.GetIngredient);
            return Problem(UrlService.GastronomyErrors.GetIngredient);
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
            _logger.LogError(e, UrlService.GastronomyErrors.GetIngredients);
            return Problem(UrlService.GastronomyErrors.GetIngredients);
        }
    }
    
    [HttpGet("list", Name = "GetPaginatedIngredients")]
    public async Task<IActionResult> GetPaginatedIngredients()
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
            _logger.LogError(e, UrlService.GastronomyErrors.GetIngredients);
            return Problem(UrlService.GastronomyErrors.GetIngredients);
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

            switch (responseCode)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.InternalServerError:
                    throw new Exception(UrlService.GastronomyErrors.InternalErrorFromService);
                default:
                    throw new Exception(UrlService.GastronomyErrors.InternalUnsupportedStatusCode);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, UrlService.GastronomyErrors.RemoveIngredient);
            return Problem(UrlService.GastronomyErrors.RemoveIngredient);
        }
    }
    
    [HttpPut(Name = "UpdateIngredient")]
    public async Task<IActionResult> UpdateIngredient([FromBody] CreateIngredientDto createIngredientDto)
    {
        try
        {

            
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{AppConstants.Services.GastronomyService} - Create Ingredient Failed");
            return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
        }
    }
    
    
    
    
    
    
}