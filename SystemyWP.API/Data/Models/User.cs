using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemyWP.API.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(256)]
        public string Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Password { get; set; }
        
        public string Image { get; set; }

        [MaxLength(512)]
        public string PasswordResetToken { get; set; }
        
        public bool EmailConfirmed { get; set; }

        public DateTime? Locked { get; set; }
        
        public DateTime? LastLogin  { get; set; }

        public List<UserClaim> Claims { get; set; } = new();

        public AccessKey AccessKey { get; set; }
        public string AccessKeyId { get; set; }
    }
}