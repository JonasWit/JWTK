using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.API.Projections.LegalApp.Cases
{
    public class LegalAppCaseProjections
    {
        public static Func<LegalAppCase, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppCase, object>> BasicProjection =>
            legalAppCase => new
            {
                legalAppCase.Id,
                legalAppCase.Name,
                legalAppCase.Signature,
            };
        
        public static Func<LegalAppCase, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppCase, object>> Projection =>
            legalAppCase => new
            {
                legalAppCase.Id,
                legalAppCase.Name,
                legalAppCase.Description,
                legalAppCase.Signature,
                legalAppCase.Updated,
                legalAppCase.UpdatedBy,
                legalAppCase.Created,
                legalAppCase.CreatedBy,
            };
    }
}