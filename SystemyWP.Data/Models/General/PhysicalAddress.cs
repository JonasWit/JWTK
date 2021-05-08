using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;

namespace Systemywp.Data.Models.General
{
    public class PhysicalAddress : BaseModel<long>
    {
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        [MaxLength(75)]
        public string Country { get; set; }
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        [Required]
        [MaxLength(50)]
        public string Building { get; set; }
        [Required]
        [MaxLength(50)]
        public string PostCode { get; set; }
    }
}