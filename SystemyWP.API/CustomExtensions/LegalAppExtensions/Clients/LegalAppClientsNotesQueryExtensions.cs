using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsNotesQueryExtensions
    {
        public static IQueryable<LegalAppClientNote> GetAllowedNote(this IQueryable<LegalAppClientNote> source,
            string userId, string role, long clientId, long noteId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappNote =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappNote =>
                            (lappNote.AuthorId.Equals(userId) || lappNote.Public) &&
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappNote.LegalAppClientId));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppClientNote> GetAllowedNotes(this IQueryable<LegalAppClientNote> source,
            string userId, string role, long clientId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappNote =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappNote =>
                            (lappNote.AuthorId.Equals(userId) || lappNote.Public) &&
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappNote.LegalAppClientId == clientId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappNote.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}