using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PortalNews : BaseModel<int>
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(10000)]
        public string News { get; set; }
    }
}