using System.Linq;
using SystemyWP.Data;
using Microsoft.Extensions.Configuration;

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