using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseContactPerson  : ContactPersonBase<int>
    {
        public int LegalAppCaseId { get; set; }
        public LegalAppCase LegalAppCase { get; set; }
    }
}