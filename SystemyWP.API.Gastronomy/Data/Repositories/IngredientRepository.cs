using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class IngredientRepository : RepositoryBase<AppDbContext>, IIngredientRepository
{
    public IngredientRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateIngredient(Ingredient ingredient) => _context.Add(ingredient);

    public Task<Ingredient> GetIngredient(ResourceAccessPass resourceAccessPass) => _context.Ingredients
            .FilterAllowedEntity(resourceAccessPass).FirstOrDefaultAsync();

    public Task<List<Ingredient>> GetIngredients(string accessKey) => _context.Ingredients
            .FilterAllowedEntities(accessKey)
            .ToListAsync();

    public Task<List<Ingredient>> GetIngredients(string accessKey, int cursor, int take) => _context.Ingredients
            .FilterAllowedEntities(accessKey)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        Ingredient ingredient = _context.Ingredients
            .FilterAllowedEntity(resourceAccessPass)
            .FirstOrDefault();
        if (ingredient is null)
        {
            return;
        }

        _ = _context.Remove(ingredient);
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        Ingredient entity =
            _context.Ingredients
                .FilterAllowedEntity(ingredient.Id, ingredient.AccessKey)
                .FirstOrDefault();
        if (entity is null)
        {
            return;
        }

        entity.Image = ingredient.Image;
        entity.Description = ingredient.Description;
        entity.Name = ingredient.Name;
        entity.MeasurementUnits = ingredient.MeasurementUnits;
        entity.StackSize = ingredient.StackSize;
        entity.PricePerStack = ingredient.PricePerStack;
        entity.Category = ingredient.Category;
    }

    public Task<int> CountIngredients(string accessKey) => _context.Ingredients.CountAsync(d => d.AccessKey == accessKey);
}