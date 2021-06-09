using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Reminders;

namespace SystemyWP.API.Projections.LegalApp.Reminders
{
    public class LegalAppRemindersProjections
    {
        public static Func<LegalAppReminder, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppReminder, object>> Projection =>
            legalAppReminder => new
            {
                legalAppReminder.UpdatedBy,
                legalAppReminder.Updated,
                legalAppReminder.Created,
                legalAppReminder.CreatedBy,
                legalAppReminder.Id,
                legalAppReminder.Active,
                legalAppReminder.Name,
                legalAppReminder.Message,
                legalAppReminder.Start,
                legalAppReminder.End,
            };
    }
}