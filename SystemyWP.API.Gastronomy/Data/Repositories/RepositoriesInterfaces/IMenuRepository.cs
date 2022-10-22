using System.Collections.Generic;
using System.Threading.Tasks;
using MasterService.API.Gastronomy.Data.DTOs;
using MasterService.API.Gastronomy.Data.Models;

namespace MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

public interface IMenuRepository : IRepositoryBase
{
    void CreateMenu(Menu menu);
    void RemoveMenu(ResourceAccessPass resourceAccessPass);
    Task<Menu> GetMenu(ResourceAccessPass resourceAccessPass);
    Task<List<Menu>> GetMenus(string accessKey);
    Task<List<Menu>> GetMenus(string accessKey, int cursor, int take);
    void UpdateMenu(Menu menu);
    void AddDish(ResourceAccessPass resourceAccessPass, long dishId); 
    void RemoveDish(ResourceAccessPass resourceAccessPass, long dishId); 
    Task<int> CountMenus(string accessKey);
}