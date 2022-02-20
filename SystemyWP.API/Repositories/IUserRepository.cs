using System;
using SystemyWP.API.Data.Models;
using SystemyWP.API.Forms;

namespace SystemyWP.API.Repositories;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(UserCredentialsForm userCredentialsForm);
    void DeleteAccount(string userId);
    void ChangePassword(string userId, string password);
    User GetUser(Func<User, bool> condition);
}