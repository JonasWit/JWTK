using System.Threading.Tasks;
using SystemyWP.Data;

namespace SystemyWP.API.Repositories;

public abstract class RepoBase
{
    protected readonly AppDbContext _context;

    protected RepoBase(AppDbContext context) => _context = context;
    
    protected Task<int> SaveChanges() => _context.SaveChangesAsync();
}