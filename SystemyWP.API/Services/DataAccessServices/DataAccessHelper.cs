using System.Threading.Tasks;
using SystemyWP.API.CustomAttributes;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Services.DataAccessServices
{
    [TransientService]
    public class DataAccessHelper
    {
        private readonly AppDbContext _context;

        public class UserAccess
        {
            public AccessKey AccessKey { get; set; }
            public User User { get; set; }
        }

        public DataAccessHelper(AppDbContext context)
        {
            _context = context;
        }

        public async Task GrabUser(string id)
        {
            
            
            
            
            
        }
        
        
    }
}