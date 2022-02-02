using System;
using System.Collections.Generic;
using System.Security.Claims;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Data.Models.UsersManagement.Access;
using SystemyWP.API.Forms.User;
using SystemyWP.API.Services.Auth;

namespace SystemyWP.API.Repositories.General;

internal class UserRepository : RepositoryBase, IUserRepository
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
    
    
}