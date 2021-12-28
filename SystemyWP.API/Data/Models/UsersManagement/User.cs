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
        [MaxLength(265)]
        public string Id { get; set; }
        
        public string Image { get; set; }

        [Required]
        [MaxLength(265)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(512)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(48)]
        public string Role { get; set; }
        
        public bool EmailConfirmed { get; set; }

        public bool IsLocked { get; set; }
        
        public DateTime? LastLogin  { get; set; }

        public List<Claim> Claims { get; set; } = new();
        
        public List<AccessKey> AccessKeys { get; set; } = new();
    }
}