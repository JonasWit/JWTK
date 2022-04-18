using System.Collections.Generic;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

public interface IDishRepository : IRepositoryBase
{
    void CreateDish(Dish dish);
    void RemoveDish(ResourceAccessPass resourceAccessPass);
    Task<Dish> GetDish(ResourceAccessPass resourceAccessPass);
    Task<List<Dish>> GetDishes(string accessKey);
    Task<Dish> UpdateDish(Dish dish);
    void AddIngredient(ResourceAccessPass resourceAccessPass, long ingredientId); 
    void RemoveIngredient(ResourceAccessPass resourceAccessPass, long ingredientId); 
}