using Domain.MasterServiceShared.DTOs;
using MasterService.API.Constants;
using MasterService.API.Data;
using MasterService.API.Data.Models;
using MasterService.API.Data.Repositories;
using MasterService.API.Services.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MasterService.Data.Repositories;

public class UserRepository : RepositoryBase<AppDbContext>
{
    private readonly Encryptor _encryptor;

    public UserRepository(
        Encryptor encryptor,
        AppDbContext context) : base(context) => _encryptor = encryptor;

    public User CreateUser(UserCredentialsForm userCredentialsForm, string confirmationToken)
    {
        var userId = Guid.NewGuid().ToString();
        var newUser = new User
        {
            Id = userId,
            Password = _encryptor.Encrypt(userCredentialsForm.Password),
            Claims = new List<UserClaim>
            {
                new()
                {
                    ClaimType = AppConstants.ClaimNames.UserAccessKey,
                    ClaimValue = Guid.NewGuid().ToString()
                },
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
                },
                new () { ClaimType = AppConstants.ClaimNames.EmailConfirmationToken, ClaimValue = confirmationToken }
            }
        };
        _ = _context.Add(newUser);
        return newUser;
    }

    public void DeleteAccount(string userId)
    {
        User user = _context.Users.FirstOrDefault(user => user.Id == userId);
        if (user is not null)
        {
            _ = _context.Remove(user);
        }
    }

    public void ConfirmEmail(string userId)
    {
        User user = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));
        if (user is null)
        {
            return;
        }

        _ = user.Claims.RemoveAll(claim => claim.ClaimType.Equals(AppConstants.ClaimNames.EmailConfirmationToken));
        user.EmailConfirmed = true;
    }

    public void ChangePassword(string userId, string password)
    {
        User user = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));
        if (user is null)
        {
            return;
        }

        _ = user.Claims.RemoveAll(claim => claim.ClaimType.Equals(AppConstants.ClaimNames.PasswordResetToken));
        user.Password = password;
    }

    public void UpdateResetPasswordToken(string userId, string token)
    {
        User user = _context.Users
            .Include(u => u.Claims)
            .FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }
        _ = user.Claims.RemoveAll(claim => claim.ClaimType.Equals(AppConstants.ClaimNames.PasswordResetToken));
        user.Claims.Add(new UserClaim { ClaimType = AppConstants.ClaimNames.PasswordResetToken, ClaimValue = token });
    }

    public User GetUser(Func<User, bool> condition) => _context.Users
        .Include(x => x.Claims)
        .FirstOrDefault(condition);

    public User GetUser(string email, string password) => _context.Users
        .Include(x => x.Claims)
        .FirstOrDefault(u => u.Claims.Any(cl => cl.ClaimType == ClaimTypes.Email && cl.ClaimValue.Equals(email)) && u.Password.Equals(password));

    public string GetUserAccessKey(string userId) => _context.UserClaims
        .FirstOrDefault(uc => uc.UserId.Equals(userId) && uc.ClaimType.Equals(AppConstants.ClaimNames.UserAccessKey))?
        .ClaimValue;

    public void RemoveClaim(string userId, string claimType) => _context.UserClaims
        .Where(claim => claim.UserId.Equals(userId))
        .ToList()
        .RemoveAll(claim => claim.ClaimType.Equals(claimType));

    public User GetUser(string email) => _context.Users
        .FirstOrDefault(user => user.Claims.Any(claim => claim.ClaimType.Equals(ClaimTypes.Email) && claim.ClaimValue.Equals(email)));

    public string GetUserId(string email) => _context.Users
        .FirstOrDefault(user => user.Claims.Any(claim => claim.ClaimType.Equals(ClaimTypes.Email) && claim.ClaimValue.Equals(email)))?.Id;

    public bool UserExists(string email) => _context.Users
        .Any(user => user.Claims.Any(claim => claim.ClaimType.Equals(ClaimTypes.Email) && claim.ClaimValue.Equals(email)));
}