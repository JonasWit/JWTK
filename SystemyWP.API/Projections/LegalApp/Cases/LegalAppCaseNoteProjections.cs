using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.API.Projections.LegalApp.Cases
{
    public class LegalAppCaseNoteProjections
    {
        public static Func<LegalAppCaseNote, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppCaseNote, object>> Projection =>
            legalAppCaseNote => new
            {
                legalAppCaseNote.Id,
                legalAppCaseNote.Created,
                legalAppCaseNote.CreatedBy,
                legalAppCaseNote.Updated,
                legalAppCaseNote.UpdatedBy,
                legalAppCaseNote.Message, 
                legalAppCaseNote.Title
            };
        
        public static Func<LegalAppCaseNote, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppCaseNote, object>> BasicProjection =>
            legalAppCaseNote => new
            {
                legalAppCaseNote.Id,
                legalAppCaseNote.Created,
                legalAppCaseNote.CreatedBy,
                legalAppCaseNote.Updated,
                legalAppCaseNote.UpdatedBy,
                legalAppCaseNote.Title
            };
    }
}