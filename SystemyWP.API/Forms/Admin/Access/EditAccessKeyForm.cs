using System;

namespace SystemyWP.API.Forms.Admin.Access
{
    public class EditAccessKeyForm
    {
        public string NewKeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}