using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.DTOs.DishDTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    private readonly ILogger<DishController> _logger;

    public DishController(
        IMapper mapper,
        IDishRepository dishRepository,
        ILogger<DishController> logger)
    {
        _mapper = mapper;
        _dishRepository = dishRepository;
        _logger = logger;
    }

    [HttpPost(Name = "CreateDish")]
    public async Task<ActionResult<DishDto>> CreateDish([FromBody] DishCreateDto dishCreateDto)
    {
        try
        {
            var dish = _mapper.Map<Dish>(dishCreateDto);
            _dishRepository.CreateDish(dish);

            if (await _dishRepository.SaveChanges() > 0) return Ok(_mapper.Map<DishDto>(dish));
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateDishException);
            return Problem(AppConstants.ResponseMessages.CreateDishException);
        }
    }

    [HttpGet("{key}/{id:long}", Name = "GetDish")]
    public async Task<ActionResult<DishDto>> GetDish(string key, long id)
    {
        try
        {
            var dish = await _dishRepository.GetDish(new ResourceAccessPass {AccessKey = key, Id = id});
            if (dish is null) return BadRequest();
            return Ok(_mapper.Map<DishDto>(dish));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetDishException);
            return Problem(AppConstants.ResponseMessages.GetDishException);
        }
    }

    [HttpGet("list/{key}", Name = "GetDishes")]
    public async Task<ActionResult<DishDto>> GetDishes(string key)
    {
        try
        {
            var dishes = await _dishRepository.GetDishes(key);
            if (dishes is null) return BadRequest();
            return Ok(_mapper.Map<List<DishDto>>(dishes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetDishesException);
            return Problem(AppConstants.ResponseMessages.GetDishesException);
        }
    }

    [HttpGet("count/{key}", Name = "CountDishes")]
    public async Task<ActionResult<int>> CountDishes(string key)
    {
        try
        {
            return Ok(new {Count = await _dishRepository.CountDishes(key)});
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CountDishesException);
            return Problem(AppConstants.ResponseMessages.CountDishesException);
        }
    }

    [HttpGet("list/{key}/{cursor:int}/{take:int}", Name = "GetPaginatedDishes")]
    public async Task<ActionResult<DishDto>> GetPaginatedDishes(string key, int cursor, int take)
    {
        try
        {
            var dishes = await _dishRepository.GetDishes(key, cursor, take);
            if (dishes is null) return BadRequest();
            return Ok(_mapper.Map<List<DishDto>>(dishes));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetDishesException);
            return Problem(AppConstants.ResponseMessages.GetDishesException);
        }
    }

    [HttpDelete("{key}/{id:long}", Name = "RemoveDish")]
    public async Task<IActionResult> RemoveDish(string key, long id)
    {
        try
        {
            _dishRepository.RemoveDish(new ResourceAccessPass {Id = id, AccessKey = key});
            if (await _dishRepository.SaveChanges() > 0) return NoContent();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.RemoveDishException);
            return Problem(AppConstants.ResponseMessages.RemoveDishException);
        }
    }

    [HttpPut(Name = "UpdateDish")]
    public async Task<IActionResult> UpdateDish([FromBody] DishBasicDto dishBasicDto)
    {
        try
        {
            _dishRepository.UpdateDish(_mapper.Map<Dish>(dishBasicDto));
            if (await _dishRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.UpdateDishException);
            return Problem(AppConstants.ResponseMessages.UpdateDishException);
        }
    }

    [HttpPost("add-ingredient", Name = "AddIngredientToDish")]
    public async Task<ActionResult> AddIngredientToDish([FromBody] DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        try
        {
            _dishRepository.AddIngredient(_mapper.Map<ResourceAccessPass>(dishIngredientUpdateDto),
                dishIngredientUpdateDto.IngredientId);
            if (await _dishRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.AddDishIngredientException);
            return Problem(AppConstants.ResponseMessages.AddDishIngredientException);
        }
    }

    [HttpPost("remove-ingredient", Name = "RemoveIngredientFromDish")]
    public async Task<ActionResult> RemoveIngredientFromDish([FromBody] DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        try
        {
            _dishRepository.RemoveIngredient(_mapper.Map<ResourceAccessPass>(dishIngredientUpdateDto),
                dishIngredientUpdateDto.IngredientId);
            if (await _dishRepository.SaveChanges() > 0) return NoContent();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.RemoveDishIngredientException);
            return Problem(AppConstants.ResponseMessages.RemoveDishIngredientException);
        }
    }
}