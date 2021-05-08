using System;
using System.Linq.Expressions;
using Systemywp.Data.DataAccessModifiers;

namespace Systemywp.Api.Projections
{
    public class DataAccessProjections
    {
        public static Func<DataAccess, object> Create => Projection.Compile();
        public static Expression<Func<DataAccess, object>> Projection =>
            data => new
            {
                RestrictedType = data.RestrictedType.ToString(),
                data.ItemId,
                data.UserId
            };
    }
}