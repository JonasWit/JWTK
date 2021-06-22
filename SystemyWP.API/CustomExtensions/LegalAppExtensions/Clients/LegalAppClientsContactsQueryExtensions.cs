using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Contacts;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients
{
    public static class LegalAppClientsContactsQueryExtensions
    {
        public static IQueryable<LegalAppContactDetails> GetAllowedContacts(
            this IQueryable<LegalAppContactDetails> source,
            string userId, string role, long clientId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClientContact.LegalAppClientId));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppContactDetails> GetAllowedContact(
            this IQueryable<LegalAppContactDetails> source,
            string userId, string role, long clientId, long contactId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappClientContact =>
                            lappClientContact.Id == contactId &&
                            lappClientContact.Active == active &&
                            lappClientContact.LegalAppClientId == context.LegalAppClients
                                .GetAllowedClient(userId, role, clientId, context, active)
                                .FirstOrDefault().Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClientContact.LegalAppClientId));
                    break;
            }

            return source;
        }
    }
}