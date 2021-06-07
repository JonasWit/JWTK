using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases
{
    public static class LegalAppCasesQueryExtensions
    {
        public static IQueryable<LegalAppCase> GetAllowedCases(this IQueryable<LegalAppCase> source, string userId,
            string role, long clientId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                            context.DataAccesses.Any(dataAccess =>
                                dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                                dataAccess.ItemId == lappCase.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppCase> GetAllowedCase(this IQueryable<LegalAppCase> source, string userId,
            string role, long clientId, long caseId, AppDbContext context, bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
                                .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source
                        .Include(x => x.LegalAppClient)
                        .Where(lappCase =>
                            lappCase.Id == caseId &&
                            lappCase.LegalAppClientId == clientId &&
                            lappCase.Active == active &&
                            lappCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                                .Include(user => user.LegalAppAccessKey)
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