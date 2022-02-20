using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Repositories;
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
            var model = _mapper.Map<Ingredient>(createIngredientDto);
            _ingredientRepository.CreateIngredient(createIngredientDto);
            await _ingredientRepository.SaveChanges();

            if (await _ingredientRepository.SaveChanges() > 0) return Ok(_mapper.Map<IngredientDto>(model));
            return BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    
    
}