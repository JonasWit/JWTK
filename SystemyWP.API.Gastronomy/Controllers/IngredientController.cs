using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.DTOs.IngredientDTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

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
    public async Task<ActionResult<IngredientDto>> CreateIngredient([FromBody] IngredientCreateDto ingredientCreateDto)
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
            _logger.LogError(e, AppConstants.ResponseMessages.CreateIngredientException);
            return Problem(AppConstants.ResponseMessages.CreateIngredientException);
        }
    }

    [HttpGet("{key}/{id:long}", Name = "GetIngredient")]
    public async Task<ActionResult<IngredientDto>> GetIngredient(string key, long id)
    {
        try
        {
            var ingredient =
                await _ingredientRepository.GetIngredient(new ResourceAccessPass {AccessKey = key, Id = id});
            if (ingredient is null) return BadRequest();
            return Ok(_mapper.Map<IngredientDto>(ingredient));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetIngredientException);
            return Problem(AppConstants.ResponseMessages.GetIngredientException);
        }
    }

    [HttpGet("list/{key}", Name = "GetIngredients")]
    public async Task<ActionResult<IngredientDto>> GetIngredients(string key)
    {
        try
        {
            var ingredients = await _ingredientRepository.GetIngredients(key);
            if (ingredients is null) return BadRequest();
            return Ok(_mapper.Map<List<IngredientDto>>(ingredients));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetIngredientException);
            return Problem(AppConstants.ResponseMessages.GetIngredientException);
        }
    }

    [HttpGet("count/{key}", Name = "CountIngredients")]
    public async Task<ActionResult<int>> CountIngredients(string key)
    {
        try
        {
            return Ok(new {Count = await _ingredientRepository.CountIngredients(key)});
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CountIngredientsException);
            return Problem(AppConstants.ResponseMessages.CountIngredientsException);
        }
    }

    [HttpGet("list/{key}/{cursor:int}/{take:int}", Name = "GetPaginatedIngredients")]
    public async Task<ActionResult<IngredientDto>> GetPaginatedIngredients(string key, int cursor, int take)
    {
        try
        {
            var ingredients = await _ingredientRepository.GetIngredients(key, cursor, take);
            if (ingredients is null) return BadRequest();
            return Ok(_mapper.Map<List<IngredientDto>>(ingredients));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetIngredientException);
            return Problem(AppConstants.ResponseMessages.GetIngredientException);
        }
    }

    [HttpDelete("{key}/{id:long}", Name = "RemoveIngredient")]
    public async Task<IActionResult> RemoveIngredient(string key, long id)
    {
        try
        {
            _ingredientRepository.RemoveIngredient(new ResourceAccessPass {Id = id, AccessKey = key});
            if (await _ingredientRepository.SaveChanges() > 0) return NoContent();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.RemoveIngredientException);
            return Problem(AppConstants.ResponseMessages.RemoveIngredientException);
        }
    }

    [HttpPut(Name = "UpdateIngredient")]
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
    {
        try
        {
            _ingredientRepository.UpdateIngredient(_mapper.Map<Ingredient>(ingredientDto));
            if (await _ingredientRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.UpdateIngredientException);
            return Problem(AppConstants.ResponseMessages.UpdateIngredientException);
        }
    }
}