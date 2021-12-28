using System.Linq;
using Microsoft.Extensions.Configuration;
using SystemyWP.API.Data;

namespace SystemyWP.API
{
    public static class DataSeed
    {
        public static void ProdAdminSeed(AppDbContext identityContext, IConfiguration config)
        {
            if (!identityContext.Users.Any(x => x.Email.Equals("biuro@systemywp.pl")))
            {

            }
        }

        public static void DevSeed(AppDbContext context)
        {
           
        
        }
    }
}