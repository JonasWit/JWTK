using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.DTOs.DishDTOs;
using SystemyWP.API.Gastronomy.Data.DTOs.MenuDTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

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
    public async Task<ActionResult<MenuDto>> CreateMenu([FromBody] MenuCreateDto menuCreateDto)
    {
        try
        {
            if (await _menuRepository.CountMenus(menuCreateDto.AccessKey) > AppConstants.RecordsLimits.Menu) return BadRequest();
            
            var menu = _mapper.Map<Menu>(menuCreateDto);
            _menuRepository.CreateMenu(menu);

            if (await _menuRepository.SaveChanges() > 0) return Ok(_mapper.Map<MenuDto>(menu));
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CreateMenu)} Error");
            return Problem($"{nameof(CreateMenu)} Error");
        }
    }

    [HttpGet("{key}/{id:long}", Name = "GetMenu")]
    public async Task<ActionResult<MenuDto>> GetMenu(string key, long id)
    {
        try
        {
            var menu = await _menuRepository.GetMenu(new ResourceAccessPass {AccessKey = key, Id = id});
            if (menu is null) return BadRequest();
            return Ok(_mapper.Map<MenuDto>(menu));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetMenu)} Error");
            return Problem($"{nameof(GetMenu)} Error");
        }
    }

    [HttpGet("list/{key}", Name = "GetMenus")]
    public async Task<ActionResult<MenuDto>> GetMenus(string key)
    {
        try
        {
            var menus = await _menuRepository.GetMenus(key);
            if (menus is null) return BadRequest();
            return Ok(_mapper.Map<List<MenuDto>>(menus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetMenus)} Error");
            return Problem($"{nameof(GetMenus)} Error");
        }
    }

    [HttpGet("count/{key}", Name = "CountMenus")]
    public async Task<ActionResult<int>> CountMenus(string key)
    {
        try
        {
            return Ok(new {Count = await _menuRepository.CountMenus(key)});
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(CountMenus)} Error");
            return Problem($"{nameof(CountMenus)} Error");
        }
    }

    [HttpGet("list/{key}/{cursor:int}/{take:int}", Name = "GetPaginatedMenus")]
    public async Task<ActionResult<MenuDto>> GetPaginatedMenus(string key, int cursor, int take)
    {
        try
        {
            var menus = await _menuRepository.GetMenus(key, cursor, take);
            if (menus is null) return BadRequest();
            return Ok(_mapper.Map<List<MenuDto>>(menus));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(GetPaginatedMenus)} Error");
            return Problem($"{nameof(GetPaginatedMenus)} Error");
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
            _logger.LogError(e, $"{nameof(RemoveMenu)} Error");
            return Problem($"{nameof(RemoveMenu)} Error");
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
            _logger.LogError(e, $"{nameof(UpdateMenu)} Error");
            return Problem($"{nameof(UpdateMenu)} Error");
        }
    }

    [HttpPost("add-dish", Name = "AddDishToMenu")]
    public async Task<IActionResult> AddDishToMenu([FromBody] MenuDishUpdateDto menuDishUpdateDto)
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
            _logger.LogError(e, $"{nameof(AddDishToMenu)} Error");
            return Problem($"{nameof(AddDishToMenu)} Error");
        }
    }

    [HttpPost("remove-dish", Name = "RemoveDishFromMenu")]
    public async Task<IActionResult> RemoveDishFromMenu([FromBody] MenuDishUpdateDto menuDishUpdateDto)
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
            _logger.LogError(e, $"{nameof(RemoveDishFromMenu)} Error");
            return Problem($"{nameof(RemoveDishFromMenu)} Error");
        }
    }
}