using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Forms;
using SystemyWP.API.HttpClients;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

namespace SystemyWP.API.Controllers;

[Authorize]
[Route("[controller]")]
public class GastronomyController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyController> _logger;

    public GastronomyController(
        IMapper mapper, 
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyController> logger)
    {
        _mapper = mapper;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }

    [HttpPost("create-ingredient", Name = "CreateIngredient")]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
    {
        try
        {
            var createdIngredient = await _gastronomyHttpClient.CreateIngredient(createIngredientDto);
            if (createdIngredient is null) throw new Exception();
            

            //return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id}, platformReadDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{SystemyWpConstants.Services.GastronomyService} - Create Ingredient Failed");
            return ServerError;
        }
    }
}