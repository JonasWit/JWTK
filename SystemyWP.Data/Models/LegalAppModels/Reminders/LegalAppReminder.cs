using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.Models.LegalAppModels.Reminders
{
    public class LegalAppReminder : ReminderBaseModel<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Public { get; set; } = false;

        [Required]
        [MaxLength(256)]
        public string AuthorId { get; set; }
        
        [Required]
        public LegalAppAccessKey LegalAppAccessKey { get; set; }
        [Required]
        public int LegalAppAccessKeyId { get; set; }
    }
}