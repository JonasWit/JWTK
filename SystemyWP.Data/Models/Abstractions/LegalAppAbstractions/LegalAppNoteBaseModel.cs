using System.ComponentModel.DataAnnotations;

namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class NoteBaseModel<TKey> : BaseModel<TKey>
    {
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}