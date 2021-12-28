using System.Threading.Tasks;

namespace SystemyWP.API.Repositories;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}