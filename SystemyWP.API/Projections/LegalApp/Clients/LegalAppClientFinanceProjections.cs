using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Clients
{
    public class LegalAppClientFinanceProjections
    {
        public static Func<LegalAppClientWorkRecord, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppClientWorkRecord, object>> BasicProjection =>
            legalAppClientFinance => new
            {
                legalAppClientFinance.Created,
                legalAppClientFinance.CreatedBy,
                legalAppClientFinance.Id,
                legalAppClientFinance.Active,
                legalAppClientFinance.Name,
            };
    }
}