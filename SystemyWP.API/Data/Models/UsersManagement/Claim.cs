using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Data.Models.UsersManagement;

public class Claim
{
    [Required]
    [MaxLength(128)]
    public string ClaimType { get; set; }
    
    [Required]  
    [MaxLength(128)]
    public string ClaimValue { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
}