using System;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.RestaurantAppModels.Access;

namespace SystemyWP.Data.Models.General
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

        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        
    }
}