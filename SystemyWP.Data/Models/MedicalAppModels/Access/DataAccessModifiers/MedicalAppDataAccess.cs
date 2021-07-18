using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.Models.MedicalAppModels.Access.DataAccessModifiers
{
    public class MedicalAppDataAccess : BaseModel<long>
    {
        public MedicalAppRestrictedType MedicalAppRestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }

        public MedicalAccessKey MedicalAccessKey { get; set; }
        public int MedicalAccessKeyId { get; set; }
    }
}