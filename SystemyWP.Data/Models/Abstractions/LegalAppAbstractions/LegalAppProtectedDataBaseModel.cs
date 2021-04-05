using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class LegalAppProtectedDataBaseModel<TKey> : TrackedModel<TKey>
    {
        [Required]
        public string DataAccessKey { get; set; }
    }
}