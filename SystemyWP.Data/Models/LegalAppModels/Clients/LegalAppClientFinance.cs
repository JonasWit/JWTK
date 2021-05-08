using Systemywp.Data.Models.Abstractions;
using Systemywp.Data.Models.General;

namespace Systemywp.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientFinance : BaseModel<long>
    {
        public User User { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }  
        public decimal Amount { get; set; }    
        
        public LegalAppClient LegalAppClient { get; set; }
    }
}