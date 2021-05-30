using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Contact;

namespace SystemyWP.API.Projections.General
{
    public class EmailProjection
    {
        public static Func<LegalAppEmailAddress, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<LegalAppEmailAddress, object>> FullProjection =>
            emailAddress => new
            {
                emailAddress.Created,
                emailAddress.CreatedBy,
                emailAddress.Id,
                emailAddress.Active,
                emailAddress.Email
            };
    }
}