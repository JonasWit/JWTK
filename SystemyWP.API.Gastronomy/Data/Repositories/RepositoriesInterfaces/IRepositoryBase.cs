using System.Threading.Tasks;

namespace MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}