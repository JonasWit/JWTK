﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClient : AccessKeyProtectedDataBaseModel<long>
    {
        [Required] [MaxLength(50)] public string Name { get; set; }

        public List<LegalAppCase> LegalAppCases { get; set; } = new List<LegalAppCase>();
        public List<ContactDetails> Contacts { get; set; } = new List<ContactDetails>();

        public List<LegalAppClientWorkRecord> LegalAppClientWorkRecords { get; set; } =
            new List<LegalAppClientWorkRecord>();

        public List<LegalAppClientNote> LegalAppClientNotes { get; set; } = new List<LegalAppClientNote>();
    }
}