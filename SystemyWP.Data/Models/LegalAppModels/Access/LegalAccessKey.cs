using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Billings;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Reminders;

namespace SystemyWP.Data.Models.LegalAppModels.Access
{
    public class LegalAccessKey : AccessKeyBase<int>
    {
        public List<LegalAppBillingRecord> LegalAppClientBillingData { get; set; } = new();
        public List<LegalAppClient> LegalAppClients { get; set; } = new();
        public List<LegalAppReminder> LegalAppReminders { get; set; } = new();
        public List<LegalAppDataAccess> LegalAppDataAccesses { get; set; } = new();
    }
}