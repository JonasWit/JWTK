using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PhysicalAddress : BaseModel<long>
    {
        public string Comment { get; set; }
        [MaxLength(75)]
        public string Country { get; set; }
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        [MaxLength(50)]
        public string Building { get; set; }
        [MaxLength(50)]
        public string PostCode { get; set; }
    }
}