using SystemyWP.API.Data.Models.UsersManagement;

namespace SystemyWP.API.Repositories.General;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(User user);
}