using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels
{
    public class LegalAppCaseContactPerson : BaseModel<int>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }

        public int LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}