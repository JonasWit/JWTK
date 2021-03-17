using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp
{
    public class LegalAppClientProjections
    {
        public static Func<LegalAppProtectedDataClient, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<LegalAppProtectedDataClient, object>> FlatProjection =>
            legalAppClient => new
            {
                legalAppClient.DataAccessKey,
                legalAppClient.Created,
                legalAppClient.CreatedBy,
                legalAppClient.Updated,
                legalAppClient.UpdatedBy,
                legalAppClient.Id,
                legalAppClient.Active,
                legalAppClient.Address,
                legalAppClient.Name,       
                legalAppClient.Email, 
                legalAppClient.PhoneNumber, 
                legalAppClient.AlternativePhoneNumber,
                Cases = legalAppClient.LegalAppCases.AsQueryable().Count()
            };
        
        public static Func<LegalAppProtectedDataClient, object> CreateMinimal => MinimalProjection.Compile();
        public static Expression<Func<LegalAppProtectedDataClient, object>> MinimalProjection =>
            legalAppClient => new
            {
                legalAppClient.Id,
                legalAppClient.DataAccessKey,
                legalAppClient.Name,
                Cases = legalAppClient.LegalAppCases
                    .AsQueryable()
                    .Where(x => x.Active)
                    .Select(LegalAppCaseProjections.MinimalProjection)
                    .ToList()
            };
    }
}