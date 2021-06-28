using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Clients
{
    public class LegalAppClientNoteProjections
    {
        public static Func<LegalAppClientNote, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppClientNote, object>> Projection =>
            legalAppClientNote => new
            {
                legalAppClientNote.Created,
                legalAppClientNote.CreatedBy,
                legalAppClientNote.UpdatedBy,
                legalAppClientNote.Updated,
                legalAppClientNote.Title,
                legalAppClientNote.Id,
                legalAppClientNote.Message,
                legalAppClientNote.Public
            };
        
        public static Func<LegalAppClientNote, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppClientNote, object>> BasicProjection =>
            legalAppClientNote => new
            {
                legalAppClientNote.Public,
                legalAppClientNote.Created,
                legalAppClientNote.CreatedBy,
                legalAppClientNote.UpdatedBy,
                legalAppClientNote.Updated,
                legalAppClientNote.Title,
                legalAppClientNote.Id,
            };
    }
}