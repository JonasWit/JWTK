using Systemywp.Data.Models.Abstractions.LegalAppAbstractions;

namespace Systemywp.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseNote : NoteBaseModel<long>
    {
        public LegalAppCase LegalAppCase { get; set; }     
    }
}