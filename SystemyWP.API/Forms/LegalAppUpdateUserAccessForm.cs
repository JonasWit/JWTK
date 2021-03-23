using System.Collections.Generic;

namespace SystemyWP.API.Forms
{
    public class LegalAppUpdateUserAccess
    {
        public string UserId { get; set; }
        public List<int> AllowedClients { get; set; } = new List<int>();
        public List<int> AllowedCases { get; set; } = new List<int>();
    }
}