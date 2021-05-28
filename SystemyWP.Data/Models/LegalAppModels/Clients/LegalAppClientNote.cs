using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientNote: NoteBaseModel<long>
    {
        public long LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}