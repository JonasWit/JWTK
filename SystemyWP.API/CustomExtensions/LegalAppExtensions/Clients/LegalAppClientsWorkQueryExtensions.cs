using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsWorkQueryExtensions
    {
        public static IQueryable<LegalAppClientWorkRecord> GetAllowedWorkRecords(
            this IQueryable<LegalAppClientWorkRecord> source,
            string userId, string role, long clientId, DateTime from, DateTime to, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.EventDate >= from && lappWorkRecord.EventDate <= to &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.EventDate >= from && lappWorkRecord.EventDate <= to &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.EventDate >= from && lappWorkRecord.EventDate <= to &&
                        lappWorkRecord.UserId.Equals(userId) &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                            dataAccess.ItemId == lappWorkRecord.LegalAppClientId));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppClientWorkRecord> GetAllowedWorkRecord(
            this IQueryable<LegalAppClientWorkRecord> source,
            string userId, string role, long clientId, long workRecordId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.LegalAppClientId == clientId &&
                        lappWorkRecord.UserId.Equals(userId) &&
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                            dataAccess.ItemId == lappWorkRecord.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}