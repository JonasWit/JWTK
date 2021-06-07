using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class ReminderBaseModel<TKey> : TrackedModel<TKey>
    {
        [Required] 
        [MaxLength(200)] 
        public string Message { get; set; }
        [Required]
        public DateTime Start { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime End { get; set; } = DateTime.UtcNow.AddMinutes(15);
    }
}