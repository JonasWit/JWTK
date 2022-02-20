using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.Lib.Shared.Abstractions.DataRelated;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

namespace SystemyWP.API.Gastronomy.Repositories;

public interface IIngredientRepository : IRepositoryBase
{
    void CreateIngredient(CreateIngredientDto createIngredientDto);
}