using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MasterService.API.Gastronomy.Data.DTOs;
using MasterService.API.Gastronomy.Data.DTOs.DishDTOs;
using MasterService.API.Gastronomy.Data.Models;
using MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace MasterService.API.Gastronomy.Controllers;

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
            if (await _dishRepository.CountDishes(dishCreateDto.AccessKey) > AppConstants.RecordsLimits.Dish) return BadRequest();
            
            var dish = _mapper.Map<Dish>(dishCreateDto);
            _dishRepository.CreateDish(dish);

            if (await _dishRepository.SaveChanges() > 0) return Ok(_mapper.Map<DishDto>(dish));
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CreateDish)} Error");
            return Problem($"{nameof(CreateDish)} Error");
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
            _logger.LogError(e, $"{nameof(GetDish)} Error");
            return Problem($"{nameof(GetDish)} Error");
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
            _logger.LogError(e, $"{nameof(GetDish)} Error");
            return Problem($"{nameof(GetDish)} Error");
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
            _logger.LogError(e, $"{nameof(CountDishes)} Error");
            return Problem($"{nameof(CountDishes)} Error");
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
            _logger.LogError(e, $"{nameof(GetPaginatedDishes)} Error");
            return Problem($"{nameof(GetPaginatedDishes)} Error");
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
            _logger.LogError(e, $"{nameof(RemoveDish)} Error");
            return Problem($"{nameof(RemoveDish)} Error");
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
            _logger.LogError(e, $"{nameof(UpdateDish)} Error");
            return Problem($"{nameof(UpdateDish)} Error");
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
            _logger.LogError(e, $"{nameof(AddIngredientToDish)} Error");
            return Problem($"{nameof(AddIngredientToDish)} Error");
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
            _logger.LogError(e, $"{nameof(RemoveIngredientFromDish)} Error");
            return Problem($"{nameof(RemoveIngredientFromDish)} Error");
        }
    }
}