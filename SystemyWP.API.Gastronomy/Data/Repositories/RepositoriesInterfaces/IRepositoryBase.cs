using System.Threading.Tasks;

namespace SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}