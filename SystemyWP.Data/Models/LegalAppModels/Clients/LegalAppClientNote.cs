using Systemywp.Data.Models.Abstractions.LegalAppAbstractions;

namespace Systemywp.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientNote: NoteBaseModel<long>
    {
        public LegalAppClient LegalAppClient { get; set; }
    }
}