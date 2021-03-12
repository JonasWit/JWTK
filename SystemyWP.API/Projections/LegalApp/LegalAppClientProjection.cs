using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels;

namespace SystemyWP.API.Projections.LegalApp
{
    public class LegalAppClientProjection
    {
        public static Func<LegalAppClient, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppClient, object>> Projection =>
            legalAppClient => new
            {
                legalAppClient.Created,
                legalAppClient.Id
            };
    }
}