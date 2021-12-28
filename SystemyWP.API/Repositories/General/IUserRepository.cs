using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Repositories.General;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(User user);
}