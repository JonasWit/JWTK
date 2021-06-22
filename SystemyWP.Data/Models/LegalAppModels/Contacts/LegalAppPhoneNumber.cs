using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Contacts
{
    public class LegalAppPhoneNumber : BaseModel<long>
    {
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(100)]
        public string Number { get; set; }
    }
}