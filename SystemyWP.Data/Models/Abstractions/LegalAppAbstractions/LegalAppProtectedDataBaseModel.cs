using Systemywp.Data.Models.General;

namespace Systemywp.Data.Models.Abstractions.LegalAppAbstractions
{
    public class LegalAppProtectedDataBaseModel<TKey> : TrackedModel<TKey>
    {
        public AccessKey AccessKey { get; set; }
        public int AccessKeyId { get; set; }
    }
}