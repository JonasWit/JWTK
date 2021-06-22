using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Contacts
{
    public class LegalAppContactDetails : BaseModel<long>
    {
        [MaxLength(300)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Surname { get; set; }
        public List<LegalAppEmailAddress> Emails { get; set; } = new();
        public List<LegalAppPhoneNumber> PhoneNumbers { get; set; } = new();
        public List<LegalAppPhysicalAddress> PhysicalAddresses { get; set; } = new();
        
        public long LegalAppClientId { get; set; }
    }
}