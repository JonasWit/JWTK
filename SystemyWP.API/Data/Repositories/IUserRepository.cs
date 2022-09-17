using System;
using SystemyWP.API.Data.DTOs.General.UserForms;
using SystemyWP.API.Data.Models;

namespace SystemyWP.API.Data.Repositories;

public interface IUserRepository : IRepositoryBase
{
    void CreateUser(UserCredentialsForm userCredentialsForm);
    void DeleteAccount(string userId);
    void UpdateResetPasswordToken(string userId, string token);
    void UpdateConfirmEmailToken(string userId, string token);
    void ChangePassword(string userId, string password);
    User GetUser(Func<User, bool> condition);
    string GetUserAccessKey(string userId);
}