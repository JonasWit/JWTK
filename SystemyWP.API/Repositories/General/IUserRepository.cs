using System;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Forms.User;

namespace SystemyWP.API.Repositories.General;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(UserCredentialsForm userCredentialsForm);
    void DeleteAccount(string userId);
    void ChangePassword(string userId, string password);
    User GetUser(Func<User, bool> condition);
}