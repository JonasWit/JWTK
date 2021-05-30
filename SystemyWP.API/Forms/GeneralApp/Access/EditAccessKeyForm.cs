using System;

namespace SystemyWP.API.Forms.GeneralApp.Access
{
    public class EditAccessKeyForm
    {
        public string OldKeyName { get; set; }  
        public string NewKeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}