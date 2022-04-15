using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Services;

namespace SystemyWP.API.Gastronomy.Repositories;

public class DishRepository : RepositoryBase<AppDbContext>, IDishRepository
{
    private readonly UrlService _urlService;

    public DishRepository(AppDbContext context, UrlService urlService) : base(context)
    {
        _urlService = urlService;
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

    public async Task<Dish> UpdateDish(Dish dish)
    {
        var entity = _context.Dishes.FirstOrDefault(e => e.Id == dish.Id && e.AccessKey == dish.AccessKey);
        if (entity is null) return await Task.FromResult<Dish>(null);

        entity.Description = dish.Description;
        entity.Name = dish.Name;

        await _context.SaveChangesAsync();
        return await GetDish(new ResourceAccessPass {Id = entity.Id, AccessKey = entity.AccessKey});
    }
    
    public Task<Dish> AddIngredient(ResourceAccessPass resourceAccessPass, Ingredient ingredient)
    {
        throw new System.NotImplementedException();
    }

    public Task<Dish> RemoveIngredient(ResourceAccessPass resourceAccessPass, Ingredient ingredient)
    {
        throw new System.NotImplementedException();
    }
}