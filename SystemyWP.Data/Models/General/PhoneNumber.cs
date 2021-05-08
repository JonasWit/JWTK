using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;

namespace Systemywp.Data.Models.General
{
    public class PhoneNumber : BaseModel<long>
    {
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(100)]
        public string Number { get; set; }
    }
}