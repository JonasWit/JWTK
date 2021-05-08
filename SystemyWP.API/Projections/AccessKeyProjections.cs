using System;
using System.Linq;
using System.Linq.Expressions;
using Systemywp.Data.Models.General;

namespace Systemywp.Api.Projections
{
    public static class AccessKeyProjection
    {
        public static Expression<Func<AccessKey, object>> FullProjection(int relatedLegalAppClients) =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsers = key.Users.AsQueryable().Count(),
                relatedLegalAppClients
            };
        
        public static Func<AccessKey, object> Create => Projection.Compile();
        public static Expression<Func<AccessKey, object>> Projection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsers = key.Users.AsQueryable().Count()
            };
        
        public static Func<AccessKey, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<AccessKey, object>> FlatProjection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
            };
    }
}