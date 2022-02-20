using System.ComponentModel.DataAnnotations;
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
    }
}