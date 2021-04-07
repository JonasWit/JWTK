using System;

namespace SystemyWP.API.Forms.Admin
{
    public class EditAccessKeyForm
    {
        public string OldKeyName { get; set; }  
        public string NewKeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}