using System.Threading.Tasks;
using SystemyWP.API.Data;

namespace SystemyWP.API.Repositories;

public abstract class RepositoryBase
{
    protected readonly AppDbContext Context;

    protected RepositoryBase(AppDbContext context) => Context = context;
    
    public Task<int> SaveChanges() => Context.SaveChangesAsync();
}