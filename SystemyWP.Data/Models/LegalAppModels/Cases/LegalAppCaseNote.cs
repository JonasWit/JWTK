using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseNote : NoteBaseModel<long>
    {
        public LegalAppCase LegalAppCase { get; set; }     
    }
}