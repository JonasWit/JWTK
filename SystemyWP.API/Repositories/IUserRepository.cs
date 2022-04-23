using System;
using SystemyWP.API.Data.Models;
using SystemyWP.API.DTOs.General.UserForms;

namespace SystemyWP.API.Repositories;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(UserCredentialsForm userCredentialsForm);
    void DeleteAccount(string userId);
    void UpdateResetPasswordTokenToken(string userId, string token);
    void ChangePassword(string userId, string password);
    User GetUser(Func<User, bool> condition);
    string GetUserAccessKey(string userId);
}