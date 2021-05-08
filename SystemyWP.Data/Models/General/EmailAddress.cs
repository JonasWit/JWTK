using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
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