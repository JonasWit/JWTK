using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Lib.Shared.Abstractions.DataRelated;

public abstract class RepositoryBase<TContext> where TContext : DbContext
{
    protected readonly TContext _context;

    protected RepositoryBase(TContext context) => _context = context;
    
    public Task<int> SaveChanges() => _context.SaveChangesAsync();
}