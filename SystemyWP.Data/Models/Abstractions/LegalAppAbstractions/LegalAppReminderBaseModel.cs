using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class ReminderBaseModel<TKey> : BaseModel<TKey>
    {
        [MaxLength(200)]
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
    }
}