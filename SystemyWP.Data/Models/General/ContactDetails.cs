using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;

namespace Systemywp.Data.Models.General
{
    public class ContactDetails : BaseModel<long>
    {
        [MaxLength(100)]
        public string Comment { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public List<EmailAddress> Emails { get; set; } = new List<EmailAddress>();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<PhysicalAddress> PhysicalAddresses { get; set; } = new List<PhysicalAddress>();
    }
}