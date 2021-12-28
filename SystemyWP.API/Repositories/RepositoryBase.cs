using System.Threading.Tasks;
using SystemyWP.API.Data;

namespace SystemyWP.API.Repositories;

public abstract class RepositoryBase
{
    protected readonly AppDbContext _context;

    protected RepositoryBase(AppDbContext context) => _context = context;
    
    public Task<int> SaveChanges() => _context.SaveChangesAsync();
}