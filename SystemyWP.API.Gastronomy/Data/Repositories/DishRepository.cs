using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class DishRepository : RepositoryBase<AppDbContext>, IDishRepository
{
    public DishRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateDish(Dish dish)
    {
        _context.Add(dish);
    }

    public void RemoveDish(ResourceAccessPass resourceAccessPass)
    {
        var dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .FirstOrDefault();
        if (dish is null) return;
        _context.Remove(dish);
    }

    public Task<Dish> GetDish(ResourceAccessPass resourceAccessPass)
    {
        return _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .FirstOrDefaultAsync();
    }

    public Task<List<Dish>> GetDishes(string accessKey)
    {
        return _context.Dishes
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .ToListAsync();
    }

    public Task<List<Dish>> GetDishes(string accessKey, int cursor, int take)
    {
        return _context.Dishes
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();
    }

    public void UpdateDish(Dish dish)
    {
        var entity = _context.Menus
            .FilterAllowedEntity(dish.Id, dish.AccessKey)
            .FirstOrDefault();
        if (entity is null) return;

        entity.Description = dish.Description;
        entity.Name = dish.Name;
    }

    public void AddIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        var dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Ingredients)
            .FirstOrDefault();
        if (dish is null) return;

        var ingredient = _context.Ingredients
            .FilterAllowedEntity(ingredientId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        dish.Ingredients.Add(ingredient);
    }

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        var dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Ingredients)
            .FirstOrDefault();
        if (dish is null) return;

        var ingredient = _context.Ingredients
            .FilterAllowedEntity(ingredientId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        dish.Ingredients.Remove(ingredient);
    }

    public Task<int> CountDishes(string accessKey)
    {
        return _context.Dishes.CountAsync(d => d.AccessKey == accessKey);
    }
}