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

    public void CreateMenu(Menu menu)=> _context.Add(menu);

    public void RemoveMenu(ResourceAccessPass resourceAccessPass)
    {
        var menu = _context.Menus.FirstOrDefault(ing =>
            ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (menu is null) return;
        _context.Remove(menu);
    }

    public Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass) => 
        _context.Menus
            .Include(d => d.Dishes)
            .FirstOrDefaultAsync(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);

    public Task<List<Menu>> GetMenus(string accessKey)=> _context.Menus
        .Where(d => d.AccessKey == accessKey)
        .Include(d => d.Dishes)
        .ToListAsync();

    public Task<List<Menu>> GetMenus(string accessKey, int cursor, int take) => 
        _context.Menus
            .Where(menu => menu.AccessKey == accessKey)
            .Include(d => d.Dishes)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();

    public void UpdateMenu(Menu menu)
    {
        var entity = _context.Menus.FirstOrDefault(e => e.Id == menu.Id && e.AccessKey == menu.AccessKey);
        if (entity is null) return;

        entity.Description = menu.Description;
        entity.Name = menu.Name;
    }

    public void AddDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        var menu = _context.Menus
            .Include(d => d.Dishes)
            .FirstOrDefault(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (menu is null) return;

        var dish = _context.Dishes.FirstOrDefault(ing => ing.Id == dishId && ing.AccessKey == resourceAccessPass.AccessKey);
        menu.Dishes.Add(dish);
    }

    public void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        var menu = _context.Menus
            .Include(d => d.Dishes)
            .FirstOrDefault(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (menu is null) return;
        
        var dish = _context.Dishes.FirstOrDefault(ing => ing.Id == dishId && ing.AccessKey == resourceAccessPass.AccessKey);
        menu.Dishes.Remove(dish);
    }

    public Task<int> CountMenus(string accessKey) => _context.Menus.CountAsync(d => d.AccessKey == accessKey);
}