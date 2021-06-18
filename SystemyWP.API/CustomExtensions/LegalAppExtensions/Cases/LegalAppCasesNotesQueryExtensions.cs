﻿using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Cases;

namespace SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases
{
    public static class LegalAppCasesNotesQueryExtensions
    {
        public static IQueryable<LegalAppCaseNote> GetAllowedNotes(
            this IQueryable<LegalAppCaseNote> source,
            string userId, string role, long caseId, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.ClientAdmin:
                    source = source.Where(legalAppCaseNote =>
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(legalAppCaseNote =>
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.Client:
                    source = source.Where(legalAppCaseNote =>
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                            dataAccess.ItemId == legalAppCaseNote.LegalAppCase.Id));
                    break;
            }

            return source;
        }
    }
}