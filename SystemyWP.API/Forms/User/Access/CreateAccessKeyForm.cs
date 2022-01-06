using System;

namespace SystemyWP.API.Forms.User.Access
{
    public class CreateAccessKeyForm
    {
        public string KeyName { get; set; }     
        public DateTime ExpireDate { get; set; }  
    }
}