using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class MenuRepository : RepositoryBase<AppDbContext>, IMenuRepository
{
    public MenuRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateMenu(Menu menu)
    {
        _context.Add(menu);
    }

    public void RemoveMenu(ResourceAccessPass resourceAccessPass)
    {
        var menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .FirstOrDefault();
        if (menu is null) return;
        _context.Remove(menu);
    }

    public Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass)
    {
        return _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefaultAsync();
    }

    public Task<List<Menu>> GetMenus(string accessKey)
    {
        return _context.Menus
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Dishes)
            .ToListAsync();
    }

    public Task<List<Menu>> GetMenus(string accessKey, int cursor, int take)
    {
        return _context.Menus
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Dishes)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();
    }

    public void UpdateMenu(Menu menu)
    {
        var entity = _context.Menus
            .FilterAllowedEntity(menu.Id, menu.AccessKey)
            .FirstOrDefault();
        if (entity is null) return;

        entity.Description = menu.Description;
        entity.Name = menu.Name;
    }

    public void AddDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        var menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefault();
        if (menu is null) return;

        var dish = _context.Dishes
            .FilterAllowedEntity(dishId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        menu.Dishes.Add(dish);
    }

    public void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        var menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefault();
        if (menu is null) return;

        var dish = _context.Dishes
            .FilterAllowedEntity(dishId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        menu.Dishes.Remove(dish);
    }

    public Task<int> CountMenus(string accessKey)
    {
        return _context.Menus.CountAsync(d => d.AccessKey == accessKey);
    }
}