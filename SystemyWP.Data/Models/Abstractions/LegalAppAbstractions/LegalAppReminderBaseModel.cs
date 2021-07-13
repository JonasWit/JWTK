using System;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Enums;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class ReminderBaseModel<TKey> : TrackedModel<TKey>
    {
        public bool AllDayEvent { get; set; }
        [Required]
        public ReminderCategory ReminderCategory { get; set; }
        [Required]
        [MaxLength(200)]
        public string Message { get; set; }
        [Required]
        public DateTime Start { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime End { get; set; } = DateTime.UtcNow.AddMinutes(15);
        
    }
}