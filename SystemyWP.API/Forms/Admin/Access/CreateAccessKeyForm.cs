using System;

namespace SystemyWP.API.Forms.Admin.Access
{
    public class CreateAccessKeyForm
    {
        public string KeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}