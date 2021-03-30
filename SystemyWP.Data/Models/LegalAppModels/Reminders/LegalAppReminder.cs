using System;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions.LegalAppAbstractions;

namespace SystemyWP.Data.Models.LegalAppModels.Reminders
{
    public class LegalAppReminder : ReminderBaseModel<long>
    {
        [MaxLength(50)] [Required] public string Name { get; set; }
    }
}