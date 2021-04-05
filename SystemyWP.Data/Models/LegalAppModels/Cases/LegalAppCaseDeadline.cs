using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseDeadline : DeadlineBaseModel<long>
    {
        public LegalAppCase LegalAppCase { get; set; }     
    }
}