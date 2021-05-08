using System.ComponentModel.DataAnnotations;
using Systemywp.Data.Models.Abstractions.LegalAppAbstractions;

namespace Systemywp.Data.Models.LegalAppModels.Reminders
{
    public class LegalAppReminder : ReminderBaseModel<long>
    {
        [MaxLength(50)] [Required] public string Name { get; set; }
    }
}