using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Repositories;

public class IngredientRepository : RepositoryBase<AppDbContext>, IIngredientRepository
{
    public IngredientRepository(AppDbContext context) : base(context)
    {
    }
    
    public void CreateIngredient(Ingredient ingredient) => _context.Add(ingredient);

    public Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass) => 
        _context.Ingredients.FirstOrDefaultAsync(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);

    public Task<List<Ingredient>> GetIngredients(string accessKey)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        var ingredient = _context.Ingredients.FirstOrDefault(ing =>
            ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (ingredient is null) return;
        _context.Remove(ingredient);
    }

    public void UpdateIngredient(Ingredient ingredient) => _context.Update(ingredient);
}