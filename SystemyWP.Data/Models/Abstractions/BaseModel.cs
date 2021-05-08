using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions
{
    public abstract class BaseModel<TKey>
    {
        public TKey Id { get; set; }
        public bool Active { get; set; } = true;
        
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}