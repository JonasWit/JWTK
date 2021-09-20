using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppCase> GetAllowedCases(this IQueryable<LegalAppCase> source, string userId,
            string role, AppDbContext context, bool active)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase &&
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase &&
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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source
                        .Where(lappCase =>
                            context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey
                                .ExpireDate >= DateTime.UtcNow &&
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClient.LegalAccessKeyId == context.Users
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAccessKey.Id &&
                            context.LegalAppDataAccesses.Any(dataAccess =>
                                dataAccess.UserId.Equals(userId) &&
                                dataAccess.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }
    }
}