using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace SystemyWP.Data.Models.General
{
    public class Person: TrackedModel<long>
    {
        [ProtectedPersonalData]
        public string Name { get; set; }
        [ProtectedPersonalData]
        public string Surname { get; set; }

        public List<EmailAddress> Emails { get; set; } = new List<EmailAddress>();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<PhysicalAddress> PhysicalAddresses { get; set; } = new List<PhysicalAddress>();
    }
}