using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientContactPerson : ContactPersonBase<int>
    {
        public int LegalAppClientId { get; set; }
        public LegalAppProtectedDataClient LegalAppProtectedDataClient { get; set; }
    }
}