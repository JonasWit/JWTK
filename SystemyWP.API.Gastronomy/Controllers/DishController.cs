using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Repositories;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly ILogger<DishController> _logger;

    public DishController(
        IMapper mapper,
        IIngredientRepository ingredientRepository,
        ILogger<DishController> logger)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
        _logger = logger;
    }

    [HttpPost(Name = "CreateDish")]
    public async Task<ActionResult<Ingredient>> CreateDish()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "CREATE Dish Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("/{key}/{id:long}", Name = "GetDish")]
    public async Task<ActionResult<Ingredient>> GetDish(string key, long id)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GET Dish Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("/{key}/{id:long}", Name = "RemoveDish")]
    public async Task<IActionResult> RemoveDish(string key, long id)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "REMOVE Dish Failed");
            return Problem("REMOVE Dish Failed");
        }
    }

    [HttpPut(Name = "UpdateDish")]
    public async Task<ActionResult<Ingredient>> UpdateDish()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "UPDATE Dish Failed");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}