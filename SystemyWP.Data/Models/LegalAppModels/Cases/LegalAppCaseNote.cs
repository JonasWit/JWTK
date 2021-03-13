using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels
{
    public class LegalAppCaseNote : BaseModel<int>
    {
        public bool Active { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        
        public int LegalAppCaseId { get; set; }
        public LegalAppCase LegalAppCase { get; set; }     
    }
}