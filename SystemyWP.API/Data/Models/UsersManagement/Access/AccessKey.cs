using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Data.Models.Abstractions;

namespace SystemyWP.API.Data.Models.UsersManagement.Access
{
    public class AccessKey : TrackedModel
    {
        [Key]
        [Required]
        [MaxLength(512)]
        public string Id { get; set; }

        public User User { get; set; }

        public List<User> AllowedUsers { get; set; }
    }
}