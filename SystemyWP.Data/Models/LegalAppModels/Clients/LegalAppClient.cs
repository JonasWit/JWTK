﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using SystemyWP.Data.Models.LegalAppModels.Contacts;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClient : TrackedModel<long>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<LegalAppCase> LegalAppCases { get; set; } = new();
        public List<LegalAppContactDetail> Contacts { get; set; } = new();

        public List<LegalAppClientWorkRecord> LegalAppClientWorkRecords { get; set; } = new();

        public List<LegalAppClientNote> LegalAppClientNotes { get; set; } = new();
        
        [Required]
        public LegalAccessKey LegalAccessKey { get; set; }
        [Required]
        public int LegalAccessKeyId { get; set; }
    }
}