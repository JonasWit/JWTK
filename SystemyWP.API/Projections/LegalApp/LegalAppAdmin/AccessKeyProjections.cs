using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.API.Projections.LegalApp.LegalAppAdmin
{
    public static class AccessKeyProjection
    {
        public static Func<AccessKeyBase<int>, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<AccessKeyBase<int>, object>> FullProjection =>
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