using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.MedicalAppModels.Access;

namespace SystemyWP.Data.DataAccessModifiers
{
    public class DataAccess : BaseModel<long>
    {
        public RestrictedType RestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }

        public LegalAppAccessKey LegalAppAccessKey { get; set; }
        public int LegalAppAccessKeyId { get; set; }
        
        public MedicalAccessKey MedicalAccessKey { get; set; }
        public int MedicalAccessKeyId { get; set; }
    }
}