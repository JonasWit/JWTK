using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;
using Systemywp.Data.Models.General;
using Systemywp.Data.Models.LegalAppModels.Clients;

namespace Systemywp.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCase : TrackedModel<long>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Signature { get; set; }
        [MaxLength(1000)]   
        public string Description { get; set; }
        
        public List<LegalAppCaseNote> LegalAppCaseNotes { get; set; } = 
            new List<LegalAppCaseNote>();
        public List<ContactDetails> Contacts { get; set; } = 
            new List<ContactDetails>();
        public List<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; } = 
            new List<LegalAppCaseDeadline>();
        
        public LegalAppClient LegalAppClient { get; set; }
    }
}