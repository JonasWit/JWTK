using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Gastronomy.Data.Models.Abstractions
{
    public abstract class TrackedModel
    {
        [Required]
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
        [Required]
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        
        [Required]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}