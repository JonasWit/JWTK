using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Clients.Cases
{
    public class LegalAppCaseNote : NoteBaseModel<long>
    {
        public long LegalAppCaseId { get; set; }
        public LegalAppCase LegalAppCase { get; set; } 
    }
}