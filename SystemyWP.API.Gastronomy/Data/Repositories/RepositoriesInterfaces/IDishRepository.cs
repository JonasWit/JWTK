using System.Collections.Generic;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

public interface IDishRepository : IRepositoryBase
{
    void CreateDish(Dish dish);
    void RemoveDish(ResourceAccessPass resourceAccessPass);
    Task<Dish> GetDish(ResourceAccessPass resourceAccessPass);
    Task<List<Dish>> GetDishes(string accessKey);
    Task<List<Dish>> GetDishes(string accessKey, int cursor, int take);
    void UpdateDish(Dish dish);
    void AddIngredient(ResourceAccessPass resourceAccessPass, long ingredientId); 
    void RemoveIngredient(ResourceAccessPass resourceAccessPass, long ingredientId); 
    Task<int> CountDishes(string accessKey);
}