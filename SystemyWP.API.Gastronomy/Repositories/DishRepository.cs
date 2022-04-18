using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;
using SystemyWP.API.Gastronomy.Services;

namespace SystemyWP.API.Gastronomy.Repositories;

public class DishRepository : RepositoryBase<AppDbContext>, IDishRepository
{
    public DishRepository(AppDbContext context) : base(context)
    {
    }
    
    public void CreateDish(Dish dish) => _context.Add(dish);
    public void RemoveDish(ResourceAccessPass resourceAccessPass)
    {
        var dish = _context.Dishes.FirstOrDefault(ing =>
            ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        if (dish is null) return;
        _context.Remove(dish);
    }

    public async Task<Dish> GetDish(ResourceAccessPass resourceAccessPass) => await _context.Dishes
        .Include(d => d.Menus)
        .Include(d => d.Ingredients)
        .FirstOrDefaultAsync(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);

    public Task<List<Dish>> GetDishes(string accessKey)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Dish> UpdateDish(Dish dish)
    {
        var entity = _context.Dishes.FirstOrDefault(e => e.Id == dish.Id && e.AccessKey == dish.AccessKey);
        if (entity is null) return await Task.FromResult<Dish>(null);

        entity.Description = dish.Description;
        entity.Name = dish.Name;

        await _context.SaveChangesAsync();
        return await GetDish(new ResourceAccessPass {Id = entity.Id, AccessKey = entity.AccessKey});
    }
    
    public void AddIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        var dish = _context.Dishes
            .Include(d => d.Ingredients)
            .First(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        var ingredient = _context.Ingredients.FirstOrDefault(ing => ing.Id == ingredientId && ing.AccessKey == resourceAccessPass.AccessKey);
        dish.Ingredients.Add(ingredient);
    }

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        var dish = _context.Dishes
            .Include(d => d.Ingredients)
            .First(ing => ing.Id == resourceAccessPass.Id && ing.AccessKey == resourceAccessPass.AccessKey);
        var ingredient = _context.Ingredients.FirstOrDefault(ing => ing.Id == ingredientId && ing.AccessKey == resourceAccessPass.AccessKey);
        dish.Ingredients.Remove(ingredient);
    }
}