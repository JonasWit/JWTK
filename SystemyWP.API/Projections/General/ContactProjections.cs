using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections.General
{
    public class ContactProjections
    {
        public static Func<ContactDetails, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<ContactDetails, object>> FullProjection =>
            contactDetails => new
            {
                contactDetails.Created,
                contactDetails.CreatedBy,
                contactDetails.Id,
                contactDetails.Active,
                contactDetails.Title,
                contactDetails.Name,
                contactDetails.Surname,
                contactDetails.Emails,
                contactDetails.PhoneNumbers,
                contactDetails.PhysicalAddresses,
                contactDetails.Comment
            };
        
        public static Func<ContactDetails, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<ContactDetails, object>> BasicProjection =>
            contactDetails => new
            {
                contactDetails.Created,
                contactDetails.CreatedBy,
                contactDetails.Id,
                contactDetails.Title,
                contactDetails.Name,
                contactDetails.Surname,
                contactDetails.Comment
            };
    }
}