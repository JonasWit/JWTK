using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly ILogger<IngredientController> _logger;

    public IngredientController(
        IMapper mapper,
        IIngredientRepository ingredientRepository,
        ILogger<IngredientController> logger)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
        _logger = logger;
    }

    [HttpPost(Name = "CreateIngredient")]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] IngredientCreateDto ingredientCreateDto)
    {
        try
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientCreateDto);
            _ingredientRepository.CreateIngredient(ingredient);
            
            if (await _ingredientRepository.SaveChanges() > 0) return Ok(_mapper.Map<IngredientDto>(ingredient));
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "CREATE Ingredient Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("/{key}/{id:long}",Name = "GetIngredient")]
    public async Task<ActionResult<Ingredient>> GetIngredient(string key, long id)
    {
        try
        {
            var ingredient = await _ingredientRepository.GetIngredient(new ResourceAccessPass {AccessKey = key, Id = id});
            if (ingredient is null) return NotFound();
            return Ok(_mapper.Map<IngredientDto>(ingredient));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GET Ingredient Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("/{key}/{id:long}", Name = "RemoveIngredient")]
    public async Task<IActionResult> RemoveIngredient(string key, long id)
    {
        try
        {
            _ingredientRepository.RemoveIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            if (await _ingredientRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "REMOVE Ingredient Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut(Name = "UpdateIngredient")]
    public async Task<ActionResult<Ingredient>> UpdateIngredient([FromBody] IngredientDto ingredientDto)
    {
        try
        {
            _ingredientRepository.UpdateIngredient(_mapper.Map<Ingredient>(ingredientDto));
            if (await _ingredientRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "UPDATE Ingredient Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}