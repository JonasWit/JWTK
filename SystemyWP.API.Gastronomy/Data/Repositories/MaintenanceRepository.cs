using System.Linq;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public class MaintenanceRepository: RepositoryBase<AppDbContext>, IMaintenanceRepository
{
    public MaintenanceRepository(AppDbContext context) : base(context)
    {
    }

    public void RemoveAllData(string accessKey)
    {
        _context.RemoveRange(_context.Ingredients.Where(ing => ing.AccessKey == accessKey));
        _context.RemoveRange(_context.Dishes.Where(ing => ing.AccessKey == accessKey));
        _context.RemoveRange(_context.Menus.Where(ing => ing.AccessKey == accessKey));
    }
}