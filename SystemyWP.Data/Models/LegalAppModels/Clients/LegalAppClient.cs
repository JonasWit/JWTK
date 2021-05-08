using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions.LegalAppAbstractions;
using Systemywp.Data.Models.General;
using Systemywp.Data.Models.LegalAppModels.Cases;

namespace Systemywp.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClient : LegalAppProtectedDataBaseModel<long>
    {
        [MaxLength(50)] [Required] public string Name { get; set; }

        public List<LegalAppCase> LegalAppCases { get; set; } =
            new List<LegalAppCase>();
        public List<ContactDetails> Contacts { get; set; } = 
            new List<ContactDetails>();
        public List<LegalAppClientFinance> LegalAppClientFinances { get; set; } =
            new List<LegalAppClientFinance>();
        public List<LegalAppClientNote> LegalAppClientNotes { get; set; } =
            new List<LegalAppClientNote>();
    }
}