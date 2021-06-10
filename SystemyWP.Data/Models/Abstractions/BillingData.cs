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

        public double PostalCode { get; set; }     
        
        public double PhoneNumber { get; set; }
        public double FaxNumber { get; set; }
        
        public double Nip { get; set; }       
        public double Regon { get; set; } 
    }
}