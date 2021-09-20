using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers
{
    public class LegalAppDataAccess : BaseModel<long>
    {
        public LegalAppRestrictedType LegalAppRestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }

        public LegalAccessKey LegalAccessKey { get; set; }
        public int LegalAccessKeyId { get; set; }
    }
}