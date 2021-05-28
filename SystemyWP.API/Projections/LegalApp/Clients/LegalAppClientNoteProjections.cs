using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Clients
{
    public class LegalAppClientNoteProjections
    {
        public static Func<LegalAppClientNote, object> CreateFull => FullProjection.Compile();
        public static Expression<Func<LegalAppClientNote, object>> FullProjection =>
            legalAppClientNote => new
            {
                legalAppClientNote.Created,
                legalAppClientNote.CreatedBy,
                legalAppClientNote.Title,
                legalAppClientNote.Id,
                legalAppClientNote.Message,
            };
        
        public static Func<LegalAppClientNote, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppClientNote, object>> BasicProjection =>
            legalAppClientNote => new
            {
                legalAppClientNote.Created,
                legalAppClientNote.CreatedBy,
                legalAppClientNote.Title,
                legalAppClientNote.Id,
            };
    }
}