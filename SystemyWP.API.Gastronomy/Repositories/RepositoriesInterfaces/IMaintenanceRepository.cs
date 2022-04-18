using System.Threading.Tasks;

namespace SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

public interface IMaintenanceRepository : IRepositoryBase
{
    void RemoveAllData(string accessKey);
}