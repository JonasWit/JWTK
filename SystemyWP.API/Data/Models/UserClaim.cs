using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Data.Models;

public class UserClaim
{
    [Key]
    public long Id { get; set; }   
    
    [Required]
    [MaxLength(128)]
    public string ClaimType { get; set; }
    
    [Required]  
    [MaxLength(128)]
    public string ClaimValue { get; set; }
    
    [MaxLength(128)]
    public string UserId { get; set; }
    public User User { get; set; }
}