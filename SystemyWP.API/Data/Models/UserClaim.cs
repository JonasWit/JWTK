using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.Models;

public class UserClaim
{
    [Key] public long Id { get; set; }

    [Required]
    [MaxLength(AppConstants.DataLimits.ShortKeyLimit)]
    public string ClaimType { get; set; }

    [Required]
    [MaxLength(AppConstants.DataLimits.ShortKeyLimit)]
    public string ClaimValue { get; set; }

    [MaxLength(AppConstants.DataLimits.ShortKeyLimit)]
    public string UserId { get; set; }

    public User User { get; set; }
}