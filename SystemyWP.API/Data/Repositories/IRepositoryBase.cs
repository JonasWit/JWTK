using System.Threading.Tasks;

namespace SystemyWP.API.Data.Repositories;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}