using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Repositories;
using SystemyWP.Lib.Shared.DTOs;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientController(
        IMapper mapper,
        IIngredientRepository ingredientRepository)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost("create-ingredient", Name = "CreateIngredient")]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
    {
        try
        {
            var ingredient = _mapper.Map<Ingredient>(createIngredientDto);
            _ingredientRepository.CreateIngredient(ingredient);
            
            if (await _ingredientRepository.SaveChanges() > 0) return Ok(_mapper.Map<IngredientDto>(ingredient));
            return BadRequest();
        }
        catch (Exception e)
        {
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
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    } 
    
}