using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Data.Repositories;

public abstract class RepositoryBase<TContext> : IRepositoryBase where TContext : DbContext 
{
    protected readonly TContext _context;

    protected RepositoryBase(TContext context) => _context = context;
    
    public Task<int> SaveChanges() => _context.SaveChangesAsync();
}