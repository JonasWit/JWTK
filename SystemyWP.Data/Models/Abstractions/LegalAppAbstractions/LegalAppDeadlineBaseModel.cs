using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class DeadlineBaseModel<TKey> : BaseModel<TKey>
    {
        [Required]
        [MaxLength(200)]
        public string Message { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}