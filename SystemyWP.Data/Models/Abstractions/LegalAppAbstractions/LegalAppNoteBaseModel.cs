﻿using System.ComponentModel.DataAnnotations;

namespace Systemywp.Data.Models.Abstractions.LegalAppAbstractions
{
    public class NoteBaseModel<TKey> : BaseModel<TKey>
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}