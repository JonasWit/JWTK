using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterService.API.Gastronomy.Data.DTOs;
using MasterService.API.Gastronomy.Data.Models;
using MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace MasterService.API.Gastronomy.Data.Repositories;

public class MenuRepository : RepositoryBase<AppDbContext>, IMenuRepository
{
    public MenuRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateMenu(Menu menu) => _context.Add(menu);

    public void RemoveMenu(ResourceAccessPass resourceAccessPass)
    {
        Menu menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .FirstOrDefault();
        if (menu is null)
        {
            return;
        }

        _ = _context.Remove(menu);
    }

    public Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass) => _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefaultAsync();

    public Task<List<Menu>> GetMenus(string accessKey) => _context.Menus
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Dishes)
            .ToListAsync();

    public Task<List<Menu>> GetMenus(string accessKey, int cursor, int take) => _context.Menus
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Dishes)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();

    public void UpdateMenu(Menu menu)
    {
        Menu entity = _context.Menus
            .FilterAllowedEntity(menu.Id, menu.AccessKey)
            .FirstOrDefault();
        if (entity is null)
        {
            return;
        }

        entity.Image = menu.Image;
        entity.Description = menu.Description;
        entity.Name = menu.Name;
        entity.Category = menu.Category;
    }

    public void AddDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        Menu menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefault();
        if (menu is null)
        {
            return;
        }

        Dish dish = _context.Dishes
            .FilterAllowedEntity(dishId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        menu.Dishes.Add(dish);
    }

    public void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        Menu menu = _context.Menus
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Dishes)
            .FirstOrDefault();
        if (menu is null)
        {
            return;
        }

        Dish dish = _context.Dishes
            .FilterAllowedEntity(dishId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        _ = menu.Dishes.Remove(dish);
    }

    public Task<int> CountMenus(string accessKey) => _context.Menus.CountAsync(d => d.AccessKey == accessKey);
}