using System;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class DeadlineBaseModel<TKey> : BaseModel<TKey>
    {
        [MaxLength(200)]
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}