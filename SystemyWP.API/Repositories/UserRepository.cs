using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models;
using SystemyWP.API.Forms;
using SystemyWP.API.Services.Auth;
using SystemyWP.Lib.Shared.Abstractions.DataRelated;

namespace SystemyWP.API.Repositories;

internal class UserRepository : RepositoryBase<AppDbContext>, IUserRepository
{
    private readonly Encryptor _encryptor;

    public UserRepository(
        Encryptor encryptor,
        AppDbContext context) : base(context)
    {
        _encryptor = encryptor;
    }
    
    public void CreateUser(UserCredentialsForm userCredentialsForm)
    {
        if(userCredentialsForm.Email is null) throw new ArgumentNullException(nameof(userCredentialsForm.Email));
        if(userCredentialsForm.Password is null) throw new ArgumentNullException(nameof(userCredentialsForm.Password));
        
        var newGuid = Guid.NewGuid().ToString();
        var newUser = new User()
        {
            Id = newGuid,
            Password = _encryptor.Encrypt(userCredentialsForm.Password),
            AccessKey = new AccessKey
            {
                Id = Guid.NewGuid().ToString(),
            },
            Claims = new List<UserClaim>
            {
                new()
                {
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = SystemyWpConstants.Roles.User
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
                    ClaimValue = newGuid
                },
            }
        };

        _context.Add(newUser);
    }

    public void DeleteAccount(string userId)
    {
        if(userId is null) throw new ArgumentNullException(nameof(userId));
        var user = _context.Users.Include(us => us.AccessKey).FirstOrDefault(user => user.Id == userId);
        if (user is not null) _context.Remove(user);
    }

    public void ChangePassword(string userId, string password)
    {
        if(userId is null) throw new ArgumentNullException(nameof(userId));
        if(password is null) throw new ArgumentNullException(nameof(password));

        var user = _context.Users.FirstOrDefault(u => u.Id.Equals(userId));
        if (user is null) return;

        user.Password = password;
        _context.Update(user);
    }
    
    public User GetUser(Func<User, bool> condition) => _context.Users
        .Include(x => x.Claims)
        .FirstOrDefault(condition);

    public string GetUserAccessKey(string userId)=> _context.AccessKeys
        .FirstOrDefault(ac => ac.User.Id == userId)?.Id;
}