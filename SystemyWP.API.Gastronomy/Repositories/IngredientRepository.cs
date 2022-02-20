using SystemyWP.API.Gastronomy.Data;
using SystemyWP.Lib.Shared.Abstractions.DataRelated;

namespace SystemyWP.API.Gastronomy.Repositories;

public class IngredientRepository : RepositoryBase<AppDbContext>,  IIngredientRepository
{
    protected IngredientRepository(AppDbContext context) : base(context)
    {
    }
    
    
    
}