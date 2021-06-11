using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Billing
{
    public class LegalAppBillingProjections
    {
        public static Func<LegalAppBillingRecord, object> CreateBasic => Projection.Compile();
        public static Expression<Func<LegalAppBillingRecord, object>> Projection =>
            legalAppBillingRecord => new
            {
                legalAppBillingRecord.Id,
                legalAppBillingRecord.Address,
                legalAppBillingRecord.City,
                legalAppBillingRecord.CreatedBy,
                legalAppBillingRecord.Created,
                legalAppBillingRecord.UpdatedBy,
                legalAppBillingRecord.Updated,
                legalAppBillingRecord.Name,
                legalAppBillingRecord.Nip,
                legalAppBillingRecord.Regon,
                legalAppBillingRecord.Street,
                legalAppBillingRecord.FaxNumber,
                legalAppBillingRecord.PhoneNumber,
                legalAppBillingRecord.PostalCode
            };
    }
}