using System;

namespace SystemyWP.API.Forms.User.Access
{
    public class EditAccessKeyForm
    {
        public string NewKeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}