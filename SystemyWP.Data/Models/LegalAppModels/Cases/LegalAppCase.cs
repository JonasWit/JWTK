using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCase : TrackedModel<long>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Signature { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(200)]
        public string Group { get; set; }
        
        public List<LegalAppCaseNote> LegalAppCaseNotes { get; set; } = new();
        
        public List<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; } = new();
     
        public long LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}