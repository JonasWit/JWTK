using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.Models;

public class User
{
    [Key]
    [Required]
    [MaxLength(AppConstants.DataLimits.KeyLimit)]
    public string Id { get; set; }

    [Required]
    [MaxLength(AppConstants.DataLimits.LongKeyLimit)]
    public string Password { get; set; }

    public string Image { get; set; }

    [MaxLength(AppConstants.DataLimits.LongKeyLimit)]
    public string PasswordResetToken { get; set; }

    public bool EmailConfirmed { get; set; }
    public DateTime? Locked { get; set; }
    public DateTime? LastLogin { get; set; }
    public List<UserClaim> Claims { get; set; } = new();

    [Required]
    [MaxLength(AppConstants.DataLimits.KeyLimit)]
    public string AccessKey { get; set; }
}