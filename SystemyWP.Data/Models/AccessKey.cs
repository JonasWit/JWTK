using System;
using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models
{
    public class AccessKey : BaseModel<int>
    {
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}