﻿using System;
using System.Linq.Expressions;
using Systemywp.Data.Models.General;

namespace Systemywp.Api.Projections.LegalApp
{
    public class LegalAppContactProjections
    {
        public static Func<ContactDetails, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<ContactDetails, object>> BasicProjection =>
            contactDetails => new
            {
                contactDetails.Created,
                contactDetails.CreatedBy,
                contactDetails.Id,
                contactDetails.Active,
                contactDetails.Name,
                contactDetails.Emails,
                contactDetails.PhoneNumbers,
                contactDetails.PhysicalAddresses,
                contactDetails.Comment
            };
    }
}