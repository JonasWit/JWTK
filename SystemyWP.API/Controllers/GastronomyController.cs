using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.DTOs;
using SystemyWP.API.DTOs.Gastronomy;
using SystemyWP.API.HttpClients;
using SystemyWP.API.Repositories;

namespace SystemyWP.API.Controllers;

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
            _logger.LogError(e, $"{SystemyWpConstants.Services.GastronomyService} - Create Ingredient Failed");
            return ServerError;
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
            _logger.LogError(e, $"{SystemyWpConstants.Services.GastronomyService} - Get Ingredient Failed");
            return ServerError;
        }
    }
}