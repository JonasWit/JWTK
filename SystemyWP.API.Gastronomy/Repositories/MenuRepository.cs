using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Menu>> GetMenus(string accessKey)
    {
        throw new System.NotImplementedException();
    }

    public Task<Menu> UpdateDish(Menu menu)
    {
        throw new System.NotImplementedException();
    }

    public Task<Menu> AddDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Menu> RemoveDish(ResourceAccessPass resourceAccessPass, long dishId)
    {
        throw new System.NotImplementedException();
    }
}