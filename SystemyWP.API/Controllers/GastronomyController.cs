using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.DTOs.Gastronomy;
using SystemyWP.API.DTOs.General;
using SystemyWP.API.Repositories;
using SystemyWP.API.Services.HttpClients;

namespace SystemyWP.API.Controllers;

[Authorize]
[Route("[controller]")]
public class GastronomyController : ApiControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyController> _logger;

    public GastronomyController(
        IUserRepository userRepository,
        IMapper mapper,
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyController> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }

    [HttpPost("create-ingredient", Name = "CreateIngredient")]
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
            _logger.LogError(e, $"{AppConstants.Services.GastronomyService} - Create Ingredient Failed");
            return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
        }
    }
    
    [HttpPost("remove-ingredient", Name = "RemoveIngredient")]
    public async Task<IActionResult> RemoveIngredient([FromBody] CreateIngredientDto createIngredientDto)
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
    
    [HttpPost("update-ingredient", Name = "UpdateIngredient")]
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
    
    [HttpGet("get-ingredient/{id:long}", Name = "GetIngredient")]
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
            _logger.LogError(e, $"{AppConstants.Services.GastronomyService} - Get Ingredient Failed");
            return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
        }
    }
}