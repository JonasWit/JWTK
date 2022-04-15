using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories;

public interface IDishRepository : IRepositoryBase
{
    void CreateDish(Dish dish);
    void RemoveDish(ResourceAccessPass resourceAccessPass);
    Task<Dish> GetDish(ResourceAccessPass resourceAccessPass);
    Task<Dish> UpdateDish(Dish dish);
    Task<Dish> AddIngredient(ResourceAccessPass resourceAccessPass, Ingredient ingredient); 
    Task<Dish> RemoveIngredient(ResourceAccessPass resourceAccessPass, Ingredient ingredient); 
}