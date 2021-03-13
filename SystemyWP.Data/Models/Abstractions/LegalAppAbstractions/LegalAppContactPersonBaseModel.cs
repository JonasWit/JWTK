using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class ContactPersonBase<TKey> : BaseModel<TKey>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(500)]
        public string Note { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
    }
}