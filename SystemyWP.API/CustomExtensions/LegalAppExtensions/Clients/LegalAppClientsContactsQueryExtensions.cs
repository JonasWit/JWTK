using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Contacts;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsContactsQueryExtensions
    {
        public static IQueryable<LegalAppContactDetail> GetAllowedContacts(
            this IQueryable<LegalAppContactDetail> source,
            string userId, string role, long clientId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == clientId &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == clientId &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == clientId &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClientContact.LegalAppClientId));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppContactDetail> GetAllowedContact(
            this IQueryable<LegalAppContactDetail> source,
            string userId, string role, long clientId, long contactId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.LegalAppClientId == clientId &&
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.LegalAppClientId == clientId &&
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.LegalAppClientId == clientId &&
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            context.LegalAppClients.FirstOrDefault(x => x.Id == lappClientContact.LegalAppClientId).LegalAppAccessKeyId == 
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClientContact.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}