using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels
{
    public class LegalAppClient : BaseModel<int>
    {
        public bool Active { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string ProfileClaim { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string DataAccessKey { get; set; }

        public List<LegalAppCase> Cases { get; set; } = new List<LegalAppCase>();
        public List<LegalAppClientContactPerson> ContactPeople { get; set; } = new List<LegalAppClientContactPerson>();
    }
}