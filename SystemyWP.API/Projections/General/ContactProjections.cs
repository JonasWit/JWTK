using System;
using System.Linq;
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
                Emails = contactDetails.Emails.AsQueryable().Select(x => new
                {
                    x.Email,
                    x.Comment,
                    x.Id,
                    x.CreatedBy
                }).ToList(),
                PhoneNumbers = contactDetails.PhoneNumbers.AsQueryable().Select(x => new
                {
                    x.Comment,
                    x.Id,
                    x.Number,
                    x.CreatedBy
                }).ToList(),
                PhysicalAddresses = contactDetails.PhysicalAddresses.AsQueryable().Select(x => new
                {
                    x.Comment,
                    x.Id,
                    x.Building,
                    x.City,
                    x.Country,
                    x.Street,
                    x.PostCode,
                    x.CreatedBy
                }).ToList(),
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