using System;
using System.Linq;
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
                legalAppCase.Group
            };
        
        public static Func<LegalAppCase, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppCase, object>> Projection =>
            legalAppCase => new
            {
                legalAppCase.Id,
                legalAppCase.Name,
                legalAppCase.Description,
                legalAppCase.Group,
                legalAppCase.Signature,
                legalAppCase.Updated,
                legalAppCase.UpdatedBy,
                legalAppCase.Created,
                legalAppCase.CreatedBy,
                Notes = legalAppCase.LegalAppCaseNotes
                    .AsQueryable()
                    .Select(LegalAppCaseNoteProjections.BasicProjection)
                    .ToList(),
                Deadlines = legalAppCase.LegalAppCaseDeadlines
                    .AsQueryable()
                    .Select(LegalAppCaseDeadlineProjections.Projection)
                    .ToList()
            };
    }
}