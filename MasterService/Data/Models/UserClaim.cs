using System.ComponentModel.DataAnnotations;

namespace MasterService.API.Data.Models;

public class UserClaim
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string ClaimType { get; set; }

    [Required]
    public string ClaimValue { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
}