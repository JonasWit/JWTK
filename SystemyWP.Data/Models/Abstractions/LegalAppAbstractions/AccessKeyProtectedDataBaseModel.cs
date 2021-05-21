using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class AccessKeyProtectedDataBaseModel<TKey> : TrackedModel<TKey>
    {
        [Required]
        public AccessKey AccessKey { get; set; }
        [Required]
        public int AccessKeyId { get; set; }
    }
}