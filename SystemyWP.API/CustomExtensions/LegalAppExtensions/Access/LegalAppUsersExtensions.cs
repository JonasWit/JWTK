using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Reminders;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Access
{
    public static class LegalAppUsersExtensions
    {
        // public static IQueryable<User> GetAllReminders(this IQueryable<LegalAppReminder> source,
        //     string userId, string role, AppDbContext context, bool active = true)
        // {
        //     switch (role)
        //     {
        //         case SystemyWpConstants.Roles.ClientAdmin:
        //             source = source.Where(lappReminder =>
        //                 lappReminder.Active == active &&
        //                 lappReminder.LegalAppAccessKeyId == context.Users
        //                     .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
        //             break;
        //         case SystemyWpConstants.Roles.PortalAdmin:
        //             break;
        //         case SystemyWpConstants.Roles.Client:
        //             break;
        //     }
        //
        //     return source;
        // }
    }
}