using System.Threading.Tasks;

namespace MasterService.API.Data.Repositories;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}