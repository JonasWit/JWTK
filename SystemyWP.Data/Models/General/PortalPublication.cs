using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class PortalPublication : TrackedModel<int>
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(5000)]
        public string News { get; set; }
        
        public string Image { get; set; }
        
        [Required]
        public PortalPublicationCategory PortalPublicationCategory { get; set; }
    }
}