using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.General.UserForms;
using SystemyWP.API.Data.Models;
using SystemyWP.API.Services.Auth;

namespace SystemyWP.API.Data.Repositories;

internal class UserRepository : RepositoryBase<AppDbContext>, IUserRepository
{
    private readonly Encryptor _encryptor;

    public UserRepository(
        Encryptor encryptor,
        AppDbContext context) : base(context) => _encryptor = encryptor;

    public void CreateUser(UserCredentialsForm userCredentialsForm)
    {
        if (userCredentialsForm.Email is null)
        {
            throw new ArgumentNullException(nameof(userCredentialsForm.Email));
        }

        if (userCredentialsForm.Password is null)
        {
            throw new ArgumentNullException(nameof(userCredentialsForm.Password));
        }

        var userId = Guid.NewGuid().ToString();
        var newUser = new User
        {
            Id = userId,
            Password = _encryptor.Encrypt(userCredentialsForm.Password),
            AccessKey = Guid.NewGuid().ToString(),
            Claims = new List<UserClaim>
            {
                new()
                {
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = AppConstants.Roles.User
                },
                new()
                {
                    ClaimType = ClaimTypes.Email,
                    ClaimValue = userCredentialsForm.Email
                },
                new()
                {
                    ClaimType = ClaimTypes.Name,
                    ClaimValue = userCredentialsForm.Email
                },
                new()
                {
                    ClaimType = ClaimTypes.NameIdentifier,
                    ClaimValue = userId
                }
            }
        };

        _ = _context.Add(newUser);
    }

    public void DeleteAccount(string userId)
    {
        if (userId is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        User user = _context.Users.FirstOrDefault(user => user.Id == userId);
        if (user is not null)
        {
            _ = _context.Remove(user);
        }
    }

    public void ChangePassword(string userId, string password)
    {
        if (userId is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        User user = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));
        if (user is null)
        {
            return;
        }

        _ = user.UserTokens.RemoveAll(ut => ut.Name.Equals(AppConstants.UserTokenNames.PasswordResetToken));
        user.Password = password;
        _ = _context.Update(user);
    }

    public void UpdateResetPasswordTokenToken(string userId, string token)
    {
        User user = _context.Users
            .Include(u => u.UserTokens)
            .FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }
        _ = user.UserTokens.RemoveAll(pt => pt.Equals(AppConstants.UserTokenNames.PasswordResetToken));
        user.UserTokens.Add(new UserToken { Name = AppConstants.UserTokenNames.PasswordResetToken, Value = token });
        _ = _context.Update(user);
    }

    public User GetUser(Func<User, bool> condition) => _context.Users
            .Include(x => x.Claims)
            .FirstOrDefault(condition);

    public string GetUserAccessKey(string userId) => _context.Users.FirstOrDefault(user => user.Id == userId)?.AccessKey;
}