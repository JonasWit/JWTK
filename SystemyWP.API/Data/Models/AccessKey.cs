using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Data.Models
{
    public class AccessKey
    {
        [Key]
        [Required]
        [MaxLength(256)]
        public string Id { get; set; }

        public User User { get; set; }
    }
}