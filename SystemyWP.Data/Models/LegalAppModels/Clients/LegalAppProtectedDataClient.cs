using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClient : LegalAppBaseModel<int>
    {
        public bool Active { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Address { get; set; }
        
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        
        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }

        public List<LegalAppCase> LegalAppCases { get; set; } = 
            new List<LegalAppCase>();
        public List<LegalAppClientContactPerson> LegalAppClientContactPersons { get; set; } = 
            new List<LegalAppClientContactPerson>();
        public List<LegalAppClientFinance> LegalAppClientFinances { get; set; } = 
            new List<LegalAppClientFinance>();
        public List<LegalAppClientNote> LegalAppClientNotes { get; set; } = 
            new List<LegalAppClientNote>();
    }
}