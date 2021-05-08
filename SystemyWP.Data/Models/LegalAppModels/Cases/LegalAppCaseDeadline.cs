using Systemywp.Data.Models.Abstractions.LegalAppAbstractions;

namespace Systemywp.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCaseDeadline : DeadlineBaseModel<long>
    {
        public LegalAppCase LegalAppCase { get; set; }     
    }
}