using System.Threading.Tasks;
using SystemyWP.API.CustomActionResult;
using SystemyWP.API.CustomAttributes;
using SystemyWP.Data;

namespace SystemyWP.API.Services.Cleaners
{
    [TransientService]
    public class UserCleaner
    {
        private readonly AppDbContext _context;

        public UserCleaner(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceActionResult> CleanUpClient(string id)
        {
 
            
            
            
            
            
            
        }
        
    }
}