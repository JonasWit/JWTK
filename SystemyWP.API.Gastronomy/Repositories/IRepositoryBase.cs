using System.Threading.Tasks;

namespace SystemyWP.API.Gastronomy.Repositories;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}