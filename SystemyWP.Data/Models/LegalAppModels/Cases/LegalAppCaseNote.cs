using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseNote : NoteBaseModel<long>
    {
        public int LegalAppCaseId { get; set; }
        public LegalAppCase LegalAppCase { get; set; }     
    }
}