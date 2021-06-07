using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsQueryExtensions
    {
        public static IQueryable<LegalAppClient> GetAllowedClients(this IQueryable<LegalAppClient> source,
            string userId, string role, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppClient> GetAllowedClient(this IQueryable<LegalAppClient> source,
            string userId, string role, long clientId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }

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
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappNote =>
                            lappNote.Id == noteId &&
                            lappNote.Active == active &&
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id &&
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
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappNote =>
                            lappNote.Active == active &&
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappNote =>
                            lappNote.Active == active &&
                            lappNote.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappNote.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}