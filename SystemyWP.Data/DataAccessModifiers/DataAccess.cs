using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.DataAccessModifiers
{
    public class DataAccess : BaseModel<long>
    {
        public RestrictedType RestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }
    }
}