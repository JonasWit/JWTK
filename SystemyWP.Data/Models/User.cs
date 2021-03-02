using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models
{
    public class User : BaseModel<string>
    {
        public string Username { get; set; }
        public string Image { get; set; }
    }
}