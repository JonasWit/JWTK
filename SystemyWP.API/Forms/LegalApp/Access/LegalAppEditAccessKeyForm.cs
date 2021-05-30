using System;

namespace SystemyWP.API.Forms.LegalApp.Access
{
    public class LegalAppEditAccessKeyForm
    {
        public string OldKeyName { get; set; }  
        public string NewKeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}