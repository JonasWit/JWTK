﻿using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.Active == active &&
                            lappClient.LegalAccessKeyId == context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappClient =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappClient.Id == clientId &&
                            lappClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppClient &&
                                dataAccess.ItemId == lappClient.Id));
                    break;
            }

            return source;
        }
    }
}