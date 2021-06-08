using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Contact;

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

        public static IQueryable<LegalAppClientWorkRecord> GetAllowedWorkRecords(
            this IQueryable<LegalAppClientWorkRecord> source,
            string userId, string role, long clientId, DateTime from, DateTime to, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.UserId.Equals(userId) &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id &&
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
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappWorkRecord =>
                        lappWorkRecord.UserId.Equals(userId) &&
                        lappWorkRecord.Id == workRecordId &&
                        lappWorkRecord.Active == active &&
                        lappWorkRecord.LegalAppClientId == context.LegalAppClients
                            .GetAllowedClient(userId, role, clientId, context, active)
                            .FirstOrDefault().Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppClient &&
                            dataAccess.ItemId == lappWorkRecord.LegalAppClientId));
                    break;
            }

            return source;
        }
        
        public static IQueryable<LegalAppContactDetails> GetAllowedContacts(this IQueryable<LegalAppContactDetails> source,
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
        
        public static IQueryable<LegalAppContactDetails> GetAllowedContact(this IQueryable<LegalAppContactDetails> source,
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