using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsQueryExtensions
    {
        public static IQueryable<LegalAppClient> GetAllowedClients(this IQueryable<LegalAppClient> source,
            string userId, string role, AppDbContext context, bool active)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClient =>
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppClient> GetAllowedClient(this IQueryable<LegalAppClient> source,
            string userId, string role, long clientId, AppDbContext context, bool active)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppClient> GetAllowedClient(this IQueryable<LegalAppClient> source,
            string userId, string role, long clientId, AppDbContext context)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClient =>
                            lappClient.Id == clientId &&
                            lappClient.LegalAppAccessKeyId == context.Users
                                .First(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }
    }
}