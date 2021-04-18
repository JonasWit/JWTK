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
                legalAppCase.Signature,
                legalAppCase.Updated,
                legalAppCase.UpdatedBy
            };
        
        public static Func<LegalAppCase, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<LegalAppCase, object>> FullProjection =>
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
                legalAppCase.Contacts,
                legalAppCase.LegalAppCaseDeadlines,
                legalAppCase.LegalAppCaseNotes
            };
    }
}