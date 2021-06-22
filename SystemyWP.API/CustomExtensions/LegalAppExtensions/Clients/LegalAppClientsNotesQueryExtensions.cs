using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
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
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappNote =>
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
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
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappNote =>
                            lappNote.Active == active &&
                            lappNote.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappNote.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}