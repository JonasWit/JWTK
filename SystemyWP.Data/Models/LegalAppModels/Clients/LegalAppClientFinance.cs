using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppClientFinance : BaseModel<long>
    {
        
        
        
        
        
        
        
        public int LegalAppClientId { get; set; }
        public LegalAppClient LegalAppClient { get; set; }
    }
}