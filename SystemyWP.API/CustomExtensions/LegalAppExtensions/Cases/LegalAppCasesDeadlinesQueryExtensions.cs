using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases
{
    public static class LegalAppCasesDeadlinesQueryExtensions
    {
        public static IQueryable<LegalAppCaseDeadline> GetAllowedDeadlines(
            this IQueryable<LegalAppCaseDeadline> source,
            string userId, string role, long caseId, DateTime from, DateTime to, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappDeadline =>
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Deadline >= from && lappDeadline.Deadline <= to &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappDeadline =>
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Deadline >= from && lappDeadline.Deadline <= to &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappDeadline =>
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Deadline >= from && lappDeadline.Deadline <= to &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                            dataAccess.ItemId == lappDeadline.LegalAppCase.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppCaseDeadline> GetAllowedDeadline(
            this IQueryable<LegalAppCaseDeadline> source,
            string userId, string role, long caseId, long deadlineId, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(lappDeadline =>
                        lappDeadline.Id == deadlineId &&
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(lappDeadline =>
                        lappDeadline.Id == deadlineId &&
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(lappDeadline =>
                        lappDeadline.Id == deadlineId &&
                        lappDeadline.LegalAppCaseId == caseId &&
                        lappDeadline.Active == active &&
                        lappDeadline.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                            dataAccess.ItemId == lappDeadline.LegalAppCase.Id));
                    break;
            }

            return source;
        }
    }
}