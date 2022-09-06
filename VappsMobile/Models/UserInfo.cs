namespace VappsMobile.Models
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime Expire { get; set; }
        public string Token { get; }

        public UserInfo(string token)
        {
            Token = token;
            Email = "test email";
        }

        private void ExtractToken()
        {

        }
    }
}
