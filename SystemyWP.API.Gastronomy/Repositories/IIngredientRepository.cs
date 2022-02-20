using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.Lib.Shared.Abstractions.DataRelated;
using SystemyWP.Lib.Shared.DTOs;

namespace SystemyWP.API.Gastronomy.Repositories;

public interface IIngredientRepository : IRepositoryBase
{
    void CreateIngredient(Ingredient createIngredientDto);
    Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass);
}