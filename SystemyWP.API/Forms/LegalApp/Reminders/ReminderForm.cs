using System;
using SystemyWP.Data.Enums;

namespace SystemyWP.API.Forms.LegalApp.Reminders
{
    public class ReminderForm
    {
        public bool AllDayEvent { get; set; } = false;
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Public  { get; set; }
        public ReminderCategory ReminderCategory { get; set; }
    }
}