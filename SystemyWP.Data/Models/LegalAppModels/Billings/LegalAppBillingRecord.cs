using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.Models.LegalAppModels.Billings
{
    public class LegalAppBillingRecord : BillingData<long>
    {
        [Required]
        public LegalAccessKey LegalAccessKey { get; set; }
        [Required]
        public int LegalAccessKeyId { get; set; }
    }
}