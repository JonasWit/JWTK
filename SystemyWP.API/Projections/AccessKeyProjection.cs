using System;
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
                key.Name,
                key.ExpireDate
            };
    }
}