using Systemywp.Data.Models.Abstractions;
using Systemywp.Data.Models.General;

namespace Systemywp.Data.DataAccessModifiers
{
    public class DataAccess : BaseModel<long>
    {
        public RestrictedType RestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }
    }
}