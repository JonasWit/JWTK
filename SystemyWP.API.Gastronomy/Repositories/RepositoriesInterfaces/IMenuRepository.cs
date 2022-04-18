using System.Collections.Generic;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

public interface IMenuRepository : IRepositoryBase
{
    void CreateMenu(Menu menu);
    void RemoveMenu(ResourceAccessPass resourceAccessPass);
    Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass);
    Task<List<Menu>> GetMenus(string accessKey);
    Task<Menu> UpdateDish(Menu menu);
    Task<Menu> AddDish(ResourceAccessPass resourceAccessPass, long dishId); 
    Task<Menu> RemoveDish(ResourceAccessPass resourceAccessPass, long dishId); 
}