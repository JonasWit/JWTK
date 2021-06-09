using System;

namespace SystemyWP.API.Forms.LegalApp.Reminders
{
    public class ReminderForm
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Public  { get; set; }
    }
}