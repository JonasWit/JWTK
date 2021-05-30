using System.Collections.Generic;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.Data.Models.LegalAppModels.Access
{
    public class LegalAppAccessKey : AccessKeyBase<int>
    {
        public List<LegalAppClient> LegalAppClients { get; set; } = new();
    }
}