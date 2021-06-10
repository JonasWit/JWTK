using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions
{
    public class BillingData<TKey> : TrackedModel<TKey>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Street { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string PostalCode { get; set; }     
        [MaxLength(50)] 
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string FaxNumber { get; set; }
        [MaxLength(50)]
        public string Nip { get; set; }    
        [MaxLength(50)]
        public string Regon { get; set; } 
    }
}