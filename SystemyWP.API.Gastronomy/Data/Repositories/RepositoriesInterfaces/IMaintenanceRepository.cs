namespace MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

public interface IMaintenanceRepository : IRepositoryBase
{
    void RemoveAllData(string accessKey);
}