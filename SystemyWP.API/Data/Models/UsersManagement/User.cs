using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data.Models.UsersManagement
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(128)]
        public string Id { get; set; }     
        
        [Required]
        [MaxLength(128)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Required]
        [MaxLength(512)]
        public string Password { get; set; }
        
        public string Image { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Role { get; set; }
        
        public string PasswordResetToken { get; set; }
        
        public bool EmailConfirmed { get; set; }

        public bool IsLocked { get; set; }
        
        public DateTime? LastLogin  { get; set; }

        public List<UserClaim> Claims { get; set; } = new();

        public List<AccessKey> AllowedKeys { get; set; }

        public AccessKey AccessKey { get; set; }
        public string AccessKeyId { get; set; }
    }
}