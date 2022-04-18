using System.Threading.Tasks;

namespace SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}