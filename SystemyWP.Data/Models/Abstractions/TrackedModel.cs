using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions
{
    public abstract class TrackedModel<TKey> : BaseModel<TKey>
    {
        [Required]
        [MaxLength(200)]
        public string UpdatedBy { get; set; }
        
        [Required]
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}