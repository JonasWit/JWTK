using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Models.Abstractions;

namespace SystemyWP.API.Data.Models.UsersManagement.Access
{
    public class AccessKey
    {
        [Key]
        [Required]
        [MaxLength(256)]
        public string Id { get; set; }

        public User User { get; set; }

        public List<User> AllowedUsers { get; set; } = new();
    }
}