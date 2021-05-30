using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Reminders
{
    public class LegalAppReminder : ReminderBaseModel<long>
    {
        [Required] 
        [MaxLength(100)] 
        public string Name { get; set; }
    }
}