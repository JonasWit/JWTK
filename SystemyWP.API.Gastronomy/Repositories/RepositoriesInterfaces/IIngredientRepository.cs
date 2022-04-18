using System.Collections.Generic;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

public interface IIngredientRepository : IRepositoryBase
{
    void CreateIngredient(Ingredient ingredient);
    Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass);
    Task<List<Ingredient>> GetIngredients(string accessKey);
    void RemoveIngredient(ResourceAccessPass resourceAccessPass);
    void UpdateIngredient(Ingredient ingredient);
}