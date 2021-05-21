using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General.Contact;

namespace SystemyWP.Data.Models.General
{
    public class Person: TrackedModel<long>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Surname { get; set; }

        public List<EmailAddress> Emails { get; set; } = new List<EmailAddress>();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<PhysicalAddress> PhysicalAddresses { get; set; } = new List<PhysicalAddress>();
    }
}