﻿using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.Models
{   // todo: add this with migration
    public class MobileLog
    {
        [Key] public long Id { get; set; }

        [Required]
        [MaxLength(AppConstants.DataLimits.DescriptionLimit)]
        public string Details { get; set; } = string.Empty;
    }
}
