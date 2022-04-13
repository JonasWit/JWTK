using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.Repositories;

public class DishRepository : RepositoryBase<AppDbContext>
{
    public DishRepository(AppDbContext context) : base(context)
    {
    }
    
    public void CreateDish(Dish dish) => _context.Add(dish);
    
    
    
}