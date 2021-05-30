using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.General
{
    public class BillingDetails : TrackedModel<long>
    {
        public string OfficialName { get; set; }
        public string Nip { get; set; }   
        public string Address { get; set; }
    }
}