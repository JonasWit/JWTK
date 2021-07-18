using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;

namespace SystemyWP.API.Projections
{
    public class DataAccessProjections
    {
        public static Func<LegalAppDataAccess, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppDataAccess, object>> Projection =>
            data => new
            {
                RestrictedType = data.LegalAppRestrictedType.ToString(),
                data.ItemId,
                data.UserId
            };
    }
}