using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.Data.Models.LegalAppModels.Cases
{
    public class LegalAppCase : BaseModel<long>
    {
        public bool Active { get; set; } = true;
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Signature { get; set; }
        [MaxLength(1000)]   
        public string Description { get; set; }
        
        public List<LegalAppCaseNote> LegalAppCaseNotes { get; set; } = 
            new List<LegalAppCaseNote>();
        public List<LegalAppCaseContactPerson> LegalAppCaseContactPersons { get; set; } = 
            new List<LegalAppCaseContactPerson>();
        public List<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; } = 
            new List<LegalAppCaseDeadline>();
        
        public int LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}