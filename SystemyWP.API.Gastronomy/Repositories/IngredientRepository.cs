using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.Lib.Shared.Abstractions.DataRelated;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

namespace SystemyWP.API.Gastronomy.Repositories;

public class IngredientRepository : RepositoryBase<AppDbContext>,  IIngredientRepository
{
    protected IngredientRepository(AppDbContext context) : base(context)
    {
    }


    public void CreateIngredient(CreateIngredientDto createIngredientDto)
    {
        throw new System.NotImplementedException();
    }
}