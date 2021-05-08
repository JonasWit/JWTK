using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;

namespace Systemywp.Data.Models.General
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