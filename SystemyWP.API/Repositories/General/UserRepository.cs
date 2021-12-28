using System;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.UsersManagement;

namespace SystemyWP.API.Repositories.General;

internal class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public void CreateUser(User user)
    {
        if(user is null) throw new ArgumentNullException(nameof(user));
        _context.Add(user);
    }
    
    
}