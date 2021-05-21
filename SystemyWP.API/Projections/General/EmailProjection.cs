using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.General.Contact;

namespace SystemyWP.API.Projections.General
{
    public class EmailProjection
    {
        public static Func<EmailAddress, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<EmailAddress, object>> FullProjection =>
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