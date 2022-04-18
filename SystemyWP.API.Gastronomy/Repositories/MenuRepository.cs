using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Repositories;

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
        _context.Menus.FirstOrDefaultAsync(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);

    public Task<List<Menu>> GetMenus(string accessKey)=> _context.Menus
        .Where(d => d.AccessKey == accessKey)
        .Include(d => d.Dishes)
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
            .First(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        var dish = _context.Dishes.FirstOrDefault(ing => ing.Id == dishId && ing.AccessKey == resourceAccessPass.AccessKey);
        menu.Dishes.Add(dish);
    }

    public void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        var menu = _context.Menus
            .Include(d => d.Dishes)
            .First(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        var dish = _context.Dishes.FirstOrDefault(ing => ing.Id == dishId && ing.AccessKey == resourceAccessPass.AccessKey);
        menu.Dishes.Remove(dish);
    }
}