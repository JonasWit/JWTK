using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.Models.LegalAppModels.Clients
{
    public class LegalAppBillingRecord : BillingData<long>
    {
        [Required]
        public LegalAppAccessKey LegalAppAccessKey { get; set; }
        [Required]
        public int LegalAppAccessKeyId { get; set; }
    }
}