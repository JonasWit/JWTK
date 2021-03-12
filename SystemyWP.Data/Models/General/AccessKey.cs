using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class AccessKey : BaseModel<int>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}