using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
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