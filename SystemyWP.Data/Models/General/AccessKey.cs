using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions;
using Systemywp.Data.Models.LegalAppModels.Clients;

namespace Systemywp.Data.Models.General
{
    public class AccessKey : TrackedModel<int>
    {
        [MaxLength(50)] [Required] public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<LegalAppClient> LegalAppClients { get; set; } = new List<LegalAppClient>();
    }
}