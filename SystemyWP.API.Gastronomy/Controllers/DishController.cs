using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.DTOs.DishDTOs;
using SystemyWP.API.Gastronomy.Repositories;

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
    public async Task<ActionResult<Ingredient>> CreateDish([FromBody] DishCreateDto dishCreateDto)
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
    public async Task<ActionResult<Ingredient>> GetDish(string key, long id)
    {
        try
        {
            var dish = await _dishRepository.GetDish(new ResourceAccessPass {AccessKey = key, Id = id});
            if (dish is null) return NotFound();
            return Ok(_mapper.Map<DishDto>(dish));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetDishException);
            return Problem(AppConstants.ResponseMessages.GetDishException);
        }
    }

    [HttpDelete("{key}/{id:long}", Name = "RemoveDish")]
    public async Task<IActionResult> RemoveDish(string key, long id)
    {
        try
        {
            _dishRepository.RemoveDish(new ResourceAccessPass {Id = id, AccessKey = key});
            if (await _dishRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.RemoveDishException);
            return Problem(AppConstants.ResponseMessages.RemoveDishException);
        }
    }

    [HttpPut(Name = "UpdateDish")]
    public async Task<ActionResult<Ingredient>> UpdateDish([FromBody] DishBasicDto dishBasicDto)
    {
        try
        {
            var dish = await _dishRepository.UpdateDish(_mapper.Map<Dish>(dishBasicDto));
            if (dish is null) return BadRequest();

            await _dishRepository.SaveChanges();
            return Ok(_mapper.Map<DishDto>(dish));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.UpdateDishException);
            return Problem(AppConstants.ResponseMessages.UpdateDishException);
        }
    }
    
    [HttpPost("add-ingredient", Name = "AddIngredient")]
    public async Task<ActionResult<Ingredient>> AddIngredient()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateDishException);
            return Problem(AppConstants.ResponseMessages.CreateDishException);
        }
    }
    
    [HttpPost("remove-ingredient", Name = "RemoveIngredient")]
    public async Task<ActionResult<Ingredient>> RemoveIngredient()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateDishException);
            return Problem(AppConstants.ResponseMessages.CreateDishException);
        }
    }
}