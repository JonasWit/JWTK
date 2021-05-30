using System;

namespace SystemyWP.API.Forms.GeneralApp.Access
{
    public class CreateAccessKeyForm
    {
        public string KeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}