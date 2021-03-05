using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels
{
    public class LegalAppCase : BaseModel<int>
    {
        public bool Active { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Signature { get; set; }
        public string Description { get; set; }
        
        public List<LegalAppCaseNote> Notes { get; set; } = new List<LegalAppCaseNote>();
        
        public int LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}