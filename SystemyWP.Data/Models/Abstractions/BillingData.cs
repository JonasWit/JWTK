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

        public int PostalCode { get; set; }     
        
        public int PhoneNumber { get; set; }
        public int FaxNumber { get; set; }
        
        public int Nip { get; set; }       
        public int Regon { get; set; } 
    }
}