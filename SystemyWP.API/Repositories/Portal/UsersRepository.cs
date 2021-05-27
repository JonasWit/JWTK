using System.Linq;
using SystemyWP.API.Repositories.Base;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Repositories.Portal
{
    public class UsersRepository : RepositoryBase
    {
        public UsersRepository(AppDbContext context, PortalLogger logger) : base(context, logger)
        {
        }

        public User GetUserProfile(string id) => _context.Users
            .Include(x => x.AccessKey)
            .FirstOrDefault(x => x.Id.Equals(id));
    }
}