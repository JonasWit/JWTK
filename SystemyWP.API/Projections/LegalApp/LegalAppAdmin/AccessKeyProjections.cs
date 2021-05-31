using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.API.Projections.LegalApp.LegalAppAdmin
{
    public static class AccessKeyProjection
    {
        public static Expression<Func<AccessKeyBase<int>, object>> FullProjection() =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
                key.Users
            };

        public static Func<AccessKeyBase<int>, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<AccessKeyBase<int>, object>> FlatProjection =>
            key => new
            {
                key.Id,
                key.Name,
                key.ExpireDate,
            };
    }
}