﻿using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases
{
    public static class LegalAppCasesQueryExtensions
    {
        public static IQueryable<LegalAppCase> GetAllowedCases(this IQueryable<LegalAppCase> source, string userId,
            string role, long clientId, AppDbContext context, bool active)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppCase> GetAllowedCase(this IQueryable<LegalAppCase> source, string userId,
            string role, long caseId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }
        
        public static IQueryable<LegalAppCase> GetAllAllowedCases(this IQueryable<LegalAppCase> source, string userId,
            string role, long caseId, AppDbContext context)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }
    }
}