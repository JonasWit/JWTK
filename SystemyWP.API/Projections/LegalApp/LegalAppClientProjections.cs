using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp
{
    public class LegalAppClientProjections
    {
        public static Func<LegalAppClient, object> CreateFlatLimited => FlatLimitedProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> FlatLimitedProjection =>
            legalAppClient => new
            {
                legalAppClient.Created,
                legalAppClient.CreatedBy,
                legalAppClient.Updated,
                legalAppClient.UpdatedBy,
                legalAppClient.Id,
                legalAppClient.Active,
                legalAppClient.Name,
            };
        
        public static Func<LegalAppClient, object> CreateFlatDetailed => FlatDetailedProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> FlatDetailedProjection =>
            legalAppClient => new
            {
                legalAppClient.Created,
                legalAppClient.CreatedBy,
                legalAppClient.Updated,
                legalAppClient.UpdatedBy,
                legalAppClient.Id,
                legalAppClient.Active,
                legalAppClient.Name,
                Cases = legalAppClient.LegalAppCases
                    .AsQueryable()
                    .Select(LegalAppCaseProjections.MinimalProjection)
                    .ToList(),
                Contacts = legalAppClient.Contacts
                    .AsQueryable()
                    .Select(LegalAppContactProjections.BasicProjection)
                    .ToList()
            };
        
        public static Func<LegalAppClient, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> BasicProjection =>
            legalAppClient => new
            {
                legalAppClient.Id,
                legalAppClient.Name,
            };

        public static Func<LegalAppClient, object> CreateMinimal => MinimalProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> MinimalProjection =>
            legalAppClient => new
            {
                legalAppClient.Id,
                legalAppClient.Name,
                Cases = legalAppClient.LegalAppCases
                    .AsQueryable()
                    .Where(x => x.Active)
                    .Select(LegalAppCaseProjections.MinimalProjection)
                    .ToList()
            };
    }
}