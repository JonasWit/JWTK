﻿using System;
using System.Linq;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Cases;
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
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappReminder =>
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappReminder =>
                        lappReminder.Active == active &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappReminder =>
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
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappReminder =>
                        lappReminder.Active == active &&
                        lappReminder.Id == reminderId &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappReminder =>
                        lappReminder.Active == active &&
                        lappReminder.Id == reminderId &&
                        lappReminder.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappReminder =>
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