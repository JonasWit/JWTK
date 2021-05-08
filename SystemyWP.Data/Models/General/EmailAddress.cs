using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;

namespace Systemywp.Data.Models.General
{
    public class EmailAddress : BaseModel<long>
    {
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}