using System.Collections.Generic;

namespace Systemywp.Api.Forms.Admin
{
    public class LegalAppUpdateUserAccessForm
    {
        public string UserId { get; set; }
        public List<int> AllowedClients { get; set; } = new List<int>();
        public List<int> AllowedCases { get; set; } = new List<int>();
    }
}