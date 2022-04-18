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
    void UpdateMenu(Menu menu);
    void AddDish(ResourceAccessPass resourceAccessPass, long dishId); 
    void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId); 
}