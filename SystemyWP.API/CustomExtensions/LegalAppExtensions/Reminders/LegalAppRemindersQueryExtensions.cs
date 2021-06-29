using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Reminders;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Reminders
{
    public static class LegalAppRemindersQueryExtensions
    {
        public static IQueryable<LegalAppReminder> GetAllReminders(this IQueryable<LegalAppReminder> source,
            string userId, string role, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        (lappReminder.AuthorId.Equals(userId) || lappReminder.Public) &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
            }

            return source;
        }
        
        public static IQueryable<LegalAppReminder> GetReminders(this IQueryable<LegalAppReminder> source,
            string userId, string role, DateTime from, DateTime to, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        ((lappReminder.End >= from && lappReminder.End <= to) || (lappReminder.Start <= to && lappReminder.Start >= from)) &&
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        ((lappReminder.End >= from && lappReminder.End <= to) || (lappReminder.Start <= to && lappReminder.Start >= from)) &&
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        ((lappReminder.End >= from && lappReminder.End <= to) || (lappReminder.Start <= to && lappReminder.Start >= from)) &&
                        lappReminder.Active == active &&
                        (lappReminder.AuthorId.Equals(userId) || lappReminder.Public) &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
            }

            return source;
        }
        
        public static IQueryable<LegalAppReminder> GetReminder(this IQueryable<LegalAppReminder> source,
            string userId, string role, long reminderId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        lappReminder.Id == reminderId &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        lappReminder.Id == reminderId &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source.Where(lappReminder =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        lappReminder.Active == active &&
                        lappReminder.Id == reminderId &&
                        (lappReminder.AuthorId.Equals(userId) || lappReminder.Public) &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
            }

            return source;
        }
    }
}