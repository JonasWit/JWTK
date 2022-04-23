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
    Task<List<Ingredient>> GetIngredients(string accessKey, int cursor, int take);
    void RemoveIngredient(ResourceAccessPass resourceAccessPass);
    void UpdateIngredient(Ingredient ingredient);
    Task<int> CountIngredients(string accessKey);
}