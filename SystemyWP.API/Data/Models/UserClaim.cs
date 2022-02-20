using System.ComponentModel.DataAnnotations;
using SystemyWP.Lib.Shared.DTOs.SharedConstants;

namespace SystemyWP.API.Data.Models;

public class UserClaim
{
    [Key] public long Id { get; set; }

    [Required]
    [MaxLength(SharedConstants.DataLimits.ShortKeyLimit)]
    public string ClaimType { get; set; }

    [Required]
    [MaxLength(SharedConstants.DataLimits.ShortKeyLimit)]
    public string ClaimValue { get; set; }

    [MaxLength(SharedConstants.DataLimits.ShortKeyLimit)]
    public string UserId { get; set; }

    public User User { get; set; }
}