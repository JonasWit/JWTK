using System.Collections.Generic;
using System.Linq;
using SystemyWP.API.Projections.LegalApp;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.CustomExtensions
{
    public static class QueryExtensions
    {
        // public static IQueryable<object> GetAllowedClientsList<T>(
        //     this IQueryable<T> source, AppDbContext context, AccessKey key, string userId, int cursor, int take,
        //     bool admin)
        // {
        //     return source
        //         .Include(x => x.AccessKey)
        //         .Where(x => x.AccessKey.Id == key.Id &&
        //                     (admin
        //                         ? true
        //                         : context.DataAccesses
        //                             .Where(y => y.UserId.Equals(userId))
        //                             .Any(y =>
        //                                 y.RestrictedType == RestrictedType.LegalAppClient && y.ItemId == x.Id)
        //                     ))
        //         .OrderBy(x => x.Name)
        //         .Skip(cursor)
        //         .Take(take)
        //         .Select(LegalAppClientProjections.FlatProjection);
        // }
    }
}