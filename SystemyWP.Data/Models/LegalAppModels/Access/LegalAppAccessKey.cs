using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Reminders;

namespace SystemyWP.Data.Models.LegalAppModels.Access
{
    public class LegalAppAccessKey : AccessKeyBase<int>
    {
        public List<LegalAppClient> LegalAppClients { get; set; } = new();
        public List<LegalAppReminder> LegalAppReminders { get; set; } = new();
    }
}