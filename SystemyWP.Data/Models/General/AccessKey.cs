using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.Data.Models.General
{
    public class AccessKey : TrackedModel<int>
    {
        [MaxLength(50)] [Required] public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<LegalAppClient> LegalAppClients { get; set; } = new List<LegalAppClient>();
    }
}