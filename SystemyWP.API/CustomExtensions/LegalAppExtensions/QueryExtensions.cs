using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions
{
    public static class QueryExtensions
    {
        public static IQueryable<LegalAppCase> GetAllowedClientItems(this IQueryable<LegalAppCase> source, string userId, AppDbContext context)
        {
           
            
            
            
            
            
           
  
                
                
                
                

            return source;
        }
    }
}