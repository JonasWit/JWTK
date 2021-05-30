using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.API.Projections.LegalApp.LegalAppAdmin
{
    public static class LegalAppAccessKeyProjection
    {
        public static Expression<Func<LegalAppAccessKey, object>> FullProjection() =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsersDetials = key.Users.Select(x => x.Id).ToList(),
            };
        
        public static Func<LegalAppAccessKey, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppAccessKey, object>> Projection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsers = key.Users.AsQueryable().Count()
            };
        
        public static Func<LegalAppAccessKey, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<LegalAppAccessKey, object>> FlatProjection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
            };
    }
}