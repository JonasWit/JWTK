using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using SystemyWP.Lib.Shared.DTOs.SharedConstants;

namespace SystemyWP.API.Data.Models
{
    public class AccessKey
    {
        [Key]
        [Required]
        [MaxLength(SharedConstants.DataLimits.KeyLimit)]
        public string Id { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }
    }
}