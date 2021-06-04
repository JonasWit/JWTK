using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> GetAllowedClientItems<T>(
            this IQueryable<T> source, 
            long itemId,
            string role, 
            AppDbContext context) where T : BaseModel<long>
        {
           
            
            
            
            
            
           
  
                
                
                
                

            return source;
        }
    }
}