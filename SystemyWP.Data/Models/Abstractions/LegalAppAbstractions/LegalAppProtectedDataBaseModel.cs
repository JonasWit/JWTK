using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class LegalAppProtectedDataBaseModel<TKey> : TrackedModel<TKey>
    {
        public AccessKey AccessKey { get; set; }
        public int AccessKeyId { get; set; }
    }
}