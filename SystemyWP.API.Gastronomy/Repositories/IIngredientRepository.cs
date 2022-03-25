using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories;

public interface IIngredientRepository : IRepositoryBase
{
    void CreateIngredient(Ingredient createIngredientDto);
    Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass);
}