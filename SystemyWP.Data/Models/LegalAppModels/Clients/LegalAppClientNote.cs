using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientNote: NoteBaseModel<long>
    {
        public bool Public { get; set; }

        [Required]
        [MaxLength(256)]
        public string AuthorId { get; set; }
        
        public long LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}