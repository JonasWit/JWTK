using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Constants;

namespace SystemyWP.API.Data.Models
{
    public class UserToken
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(AppConstants.DataLimits.NameLimit)]
        public string Name { get; set; }

        [Required]
        [MaxLength(AppConstants.DataLimits.DescriptionLimit)]
        public string Value { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
