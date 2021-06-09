using System.Collections.Generic;

namespace SystemyWP.API.Forms.Admin
{
    public class LegalAppUpdateUserAccessForm
    {
        public string UserId { get; set; }
        public List<long> AllowedClients { get; set; } = new();
        public List<long> AllowedCases { get; set; } = new();
    }
}