using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
{
    public static class AccessKeyProjection
    {
        public static Expression<Func<AccessKey, object>> FullProjection() =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsersDetials = key.Users.Select(x => x.Id).ToList(),
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