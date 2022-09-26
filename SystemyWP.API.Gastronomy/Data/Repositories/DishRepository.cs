using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class DishRepository : RepositoryBase<AppDbContext>, IDishRepository
{
    public DishRepository(AppDbContext context) : base(context)
    {
    }

    public void CreateDish(Dish dish) => _context.Add(dish);

    public void RemoveDish(ResourceAccessPass resourceAccessPass)
    {
        Dish dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .FirstOrDefault();
        if (dish is null)
        {
            return;
        }

        _ = _context.Remove(dish);
    }

    public Task<Dish> GetDish(ResourceAccessPass resourceAccessPass) => _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .FirstOrDefaultAsync();

    public Task<List<Dish>> GetDishes(string accessKey) => _context.Dishes
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .ToListAsync();

    public Task<List<Dish>> GetDishes(string accessKey, int cursor, int take) => _context.Dishes
            .FilterAllowedEntities(accessKey)
            .Include(d => d.Menus)
            .Include(d => d.Ingredients)
            .OrderBy(x => x.Id)
            .Skip(cursor)
            .Take(take)
            .ToListAsync();

    public void UpdateDish(Dish dish)
    {
        Menu entity = _context.Menus
            .FilterAllowedEntity(dish.Id, dish.AccessKey)
            .FirstOrDefault();
        if (entity is null)
        {
            return;
        }

        entity.Image = dish.Image;
        entity.Description = dish.Description;
        entity.Name = dish.Name;
        entity.Category = dish.Category;
    }

    public void AddIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        Dish dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Ingredients)
            .FirstOrDefault();
        if (dish is null)
        {
            return;
        }

        Ingredient ingredient = _context.Ingredients
            .FilterAllowedEntity(ingredientId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        dish.Ingredients.Add(ingredient);
    }

    public void RemoveIngredient(ResourceAccessPass resourceAccessPass, long ingredientId)
    {
        Dish dish = _context.Dishes
            .FilterAllowedEntity(resourceAccessPass)
            .Include(d => d.Ingredients)
            .FirstOrDefault();
        if (dish is null)
        {
            return;
        }

        Ingredient ingredient = _context.Ingredients
            .FilterAllowedEntity(ingredientId, resourceAccessPass.AccessKey)
            .FirstOrDefault();
        _ = dish.Ingredients.Remove(ingredient);
    }

    public Task<int> CountDishes(string accessKey) => _context.Dishes.CountAsync(d => d.AccessKey == accessKey);
}