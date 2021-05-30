using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Contact;

namespace SystemyWP.API.Projections.General
{
    public class ContactProjections
    {
        public static Func<LegalAppContactDetails, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<LegalAppContactDetails, object>> FullProjection =>
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
                    x.Comment,
                    x.Email,
                    x.CreatedBy,
                    x.Id
                }).ToList(),
                PhoneNumbers = contactDetails.PhoneNumbers.AsQueryable().Select(x => new
                {
                    x.Id,
                    x.Comment,
                    x.CreatedBy,
                    x.Number
                }).ToList(),
                PhysicalAddresses = contactDetails.PhysicalAddresses.AsQueryable().Select(x => new
                {
                    x.Id,
                    x.Comment,
                    x.CreatedBy,
                    x.Building,
                    x.City,
                    x.Country,
                    x.Street,
                    x.PostCode
                }).ToList(),
                contactDetails.Comment
            };
        
        public static Func<LegalAppContactDetails, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppContactDetails, object>> BasicProjection =>
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