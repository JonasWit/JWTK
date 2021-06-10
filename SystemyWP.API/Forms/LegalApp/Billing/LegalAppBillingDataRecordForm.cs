namespace SystemyWP.API.Forms.LegalApp.Billing
{
    public class LegalAppBillingDataRecordForm
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        
        public string City { get; set; }

        public int PostalCode { get; set; }     
        
        public int PhoneNumber { get; set; }
        public int FaxNumber { get; set; }
        
        public int Nip { get; set; }      
        public int Regon { get; set; }
    }
}