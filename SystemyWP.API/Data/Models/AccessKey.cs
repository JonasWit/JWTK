using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.Models
{
    public class AccessKey
    {
        [Key]
        [Required]
        [MaxLength(AppConstants.DataLimits.KeyLimit)]
        public string Id { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }
    }
}