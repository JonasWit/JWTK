using System;

namespace SystemyWP.API.Forms.LegalApp.Client
{
    public class ClientWorkForm
    {
        public string Name { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }  
        public decimal Amount { get; set; } 
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}