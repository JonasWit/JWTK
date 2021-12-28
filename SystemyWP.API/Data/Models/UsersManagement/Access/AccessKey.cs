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
        public long Id { get; set; }  
        
        [Required] 
        [MaxLength(100)] 
        public string Name { get; set; }

        [Required] 
        public ApplicationType ApplicationType { get; set; }
        
        public List<User> Users { get; set; } = new();
    }
}