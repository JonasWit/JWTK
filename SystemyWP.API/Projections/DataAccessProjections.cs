using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
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