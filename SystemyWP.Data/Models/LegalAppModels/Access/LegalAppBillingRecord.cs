using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Access
{
    public class LegalAppBillingRecord : BillingData<long>
    {
        [Required]
        public LegalAppAccessKey LegalAppAccessKey { get; set; }
        [Required]
        public int LegalAppAccessKeyId { get; set; }
    }
}