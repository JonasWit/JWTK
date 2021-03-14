using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Reminders
{
    public class LegalAppReminder : ReminderBaseModel<long>
    {
        public bool Public { get; set; }
    }
}