using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Projections
{
    public static class AccessKeyProjection
    {
        public static Func<AccessKey, object> Create => Projection.Compile();
        public static Expression<Func<AccessKey, object>> Projection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                assignedUsers = key.Users.AsQueryable().Count()
            };
    }
}