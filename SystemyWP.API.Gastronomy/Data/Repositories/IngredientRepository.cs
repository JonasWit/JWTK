using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class IngredientRepository : RepositoryBase<AppDbContext>, IIngredientRepository
{
    public IngredientRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateIngredient(Ingredient ingredient)
    {
        _context.Add(ingredient);
    }

    public Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass)
    {
        return _context.Ingredients.FirstOrDefaultAsync(ing =>
            ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
    }

    public Task<List<Ingredient>> GetIngredients(string accessKey)
    {
        return _context.Ingredients
            .Where(d => d.AccessKey == accessKey)
            .ToListAsync();
    }

    public Task<List<Ingredient>> GetIngredients(string accessKey, int cursor, int take)
    {
        return _context.Ingredients
            .Where(menu => menu.AccessKey == accessKey)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();
    }

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        var ingredient = _context.Ingredients.FirstOrDefault(ing =>
            ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (ingredient is null) return;
        _context.Remove(ingredient);
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        var entity =
            _context.Ingredients.FirstOrDefault(ing =>
                ing.Id == ingredient.Id && ing.AccessKey == ingredient.AccessKey);
        if (entity is null) return;

        entity.Description = ingredient.Description;
        entity.Name = ingredient.Name;
        entity.MeasurementUnits = ingredient.MeasurementUnits;
        entity.StackSize = ingredient.StackSize;
        entity.PricePerStack = ingredient.PricePerStack;
    }

    public Task<int> CountIngredients(string accessKey)
    {
        return _context.Ingredients.CountAsync(d => d.AccessKey == accessKey);
    }
}