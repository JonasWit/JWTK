using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.API.Projections.LegalApp
{
    public class LegalAppCaseProjections
    {
        public static Func<LegalAppCase, object> CreateMinimal => MinimalProjection.Compile();
        public static Expression<Func<LegalAppCase, object>> MinimalProjection =>
            legalAppCase => new
            {
                legalAppCase.Id,
                legalAppCase.Name,
            };
    }
}