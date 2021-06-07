using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions
{
    public static class QueryExtensions
    {
        public static IQueryable<LegalAppClient> GetAllowedClients(this IQueryable<LegalAppClient> source, string userId, string role, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin :
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin :
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;              
                case SystemyWpConstants.Roles.Client :
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess => dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                                                   dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }
        
        // public static IQueryable<LegalAppCase> GetAllowedCases(this IQueryable<LegalAppCase> source, string userId, string role, AppDbContext context, bool active = true)
        // {
        //     var cases = _context.LegalAppCases
        //         .Include(x => x.LegalAppClient)
        //         .Where(x =>
        //             x.Active &&
        //             x.LegalAppClient.Id == clientId &&
        //             x.LegalAppClient.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
        //         .Select(LegalAppCaseProjections.Projection)
        //         .ToList();
        //     
        //     switch (role)
        //     {
        //         case SystemyWpConstants.Roles.ClientAdmin :
        //             source = source
        //                 .Include(x => x.LegalAppClient)
        //                 .Where(lappCase => 
        //                     
        //                 );
        //             break;
        //         case SystemyWpConstants.Roles.PortalAdmin :
        //
        //             break;              
        //         case SystemyWpConstants.Roles.Client :
        //
        //             break;
        //     }
        //
        //     return source;
        // }
    }
}