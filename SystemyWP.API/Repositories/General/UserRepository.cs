using System;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;

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