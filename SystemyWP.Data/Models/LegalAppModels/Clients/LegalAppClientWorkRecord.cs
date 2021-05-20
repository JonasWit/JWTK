using System;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientWorkRecord : BaseModel<long>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserEmail { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
   
        public long LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}