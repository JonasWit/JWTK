using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.DTOs.DishDTOs;
using SystemyWP.API.Gastronomy.DTOs.MenuDTOs;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMenuRepository _menuRepository;
    private readonly ILogger<MenuController> _logger;

    public MenuController(
        IMapper mapper,
        IMenuRepository menuRepository,
        ILogger<MenuController> logger)
    {
        _mapper = mapper;
        _menuRepository = menuRepository;
        _logger = logger;
    }

    [HttpPost(Name = "CreateMenu")]
    public async Task<ActionResult<Ingredient>> CreateMenu([FromBody] MenuCreateDto menuCreateDto)
    {
        try
        {
            var menu = _mapper.Map<Menu>(menuCreateDto);
            _menuRepository.CreateMenu(menu);

            if (await _menuRepository.SaveChanges() > 0) return Ok(_mapper.Map<MenuDto>(menu));
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateMenuException);
            return Problem(AppConstants.ResponseMessages.CreateMenuException);
        }
    }

    [HttpGet("{key}/{id:long}", Name = "GetMenu")]
    public async Task<ActionResult<Ingredient>> GetMenu(string key, long id)
    {
        try
        {
            var menu = await _menuRepository.GetMenu(new ResourceAccessPass {AccessKey = key, Id = id});
            if (menu is null) return NotFound();
            return Ok(_mapper.Map<MenuDto>(menu));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetMenuException);
            return Problem(AppConstants.ResponseMessages.GetMenuException);
        }
    }

    [HttpGet("list/{key}", Name = "GetMenus")]
    public async Task<ActionResult<DishDto>> GetMenus(string key)
    {
        try
        {
            return Ok(new {Count = await _menuRepository.CountMenus(key)});
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CountMenusException);
            return Problem(AppConstants.ResponseMessages.CountMenusException);
        }
    }

    [HttpGet("count/{key}", Name = "CountMenus")]
    public async Task<ActionResult<int>> CountMenus(string key)
    {
        try
        {
            var menus = await _menuRepository.GetMenus(key);
            if (menus is null) return NotFound();
            return Ok(_mapper.Map<List<MenuDto>>(menus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetMenusException);
            return Problem(AppConstants.ResponseMessages.GetMenusException);
        }
    }

    [HttpGet("list/{key}/{cursor:int}/{take:int}", Name = "GetPaginatedMenus")]
    public async Task<ActionResult<DishDto>> GetPaginatedMenus(string key, int cursor, int take)
    {
        try
        {
            var menus = await _menuRepository.GetMenus(key, cursor, take);
            if (menus is null) return NotFound();
            return Ok(_mapper.Map<List<MenuDto>>(menus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.GetMenusException);
            return Problem(AppConstants.ResponseMessages.GetMenusException);
        }
    }

    [HttpDelete("{key}/{id:long}", Name = "RemoveMenu")]
    public async Task<IActionResult> RemoveMenu(string key, long id)
    {
        try
        {
            _menuRepository.RemoveMenu(new ResourceAccessPass {Id = id, AccessKey = key});
            if (await _menuRepository.SaveChanges() > 0) return NoContent();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.RemoveDishException);
            return Problem(AppConstants.ResponseMessages.RemoveDishException);
        }
    }

    [HttpPut(Name = "UpdateMenu")]
    public async Task<IActionResult> UpdateMenu([FromBody] MenuBasicDto menuBasicDto)
    {
        try
        {
            _menuRepository.UpdateMenu(_mapper.Map<Menu>(menuBasicDto));
            if (await _menuRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.UpdateMenuException);
            return Problem(AppConstants.ResponseMessages.UpdateMenuException);
        }
    }

    [HttpPost("add-dish", Name = "AddDishToMenu")]
    public async Task<ActionResult<Dish>> AddDishToMenu(
        [FromBody] MenuDishUpdateDto menuDishUpdateDto)
    {
        try
        {
            _menuRepository.AddDish(_mapper.Map<ResourceAccessPass>(menuDishUpdateDto),
                menuDishUpdateDto.DishId);
            if (await _menuRepository.SaveChanges() > 0) return Ok();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateDishException);
            return Problem(AppConstants.ResponseMessages.CreateDishException);
        }
    }

    [HttpPost("remove-dish", Name = "RemoveDishFromMenu")]
    public async Task<ActionResult<Dish>> RemoveDishFromMenu(
        [FromBody] MenuDishUpdateDto menuDishUpdateDto)
    {
        try
        {
            _menuRepository.RemoveDish(_mapper.Map<ResourceAccessPass>(menuDishUpdateDto),
                menuDishUpdateDto.DishId);
            if (await _menuRepository.SaveChanges() > 0) return NoContent();
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.CreateDishException);
            return Problem(AppConstants.ResponseMessages.CreateDishException);
        }
    }
}