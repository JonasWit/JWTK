﻿using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;

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
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.UserId.Equals(userId) &&
                            dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                            dataAccess.ItemId == legalAppCaseNote.LegalAppCase.Id));
                    break;
            }

            return source;
        }

        public static IQueryable<LegalAppCaseNote> GetAllowedNote(
            this IQueryable<LegalAppCaseNote> source,
            string userId, string role, long caseId, long noteId, AppDbContext context,
            bool active = true)
        {
            switch (role)
            {
                case SystemyWpConstants.Roles.UserAdmin:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.Id == noteId &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.PortalAdmin:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.Id == noteId &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id);
                    break;
                case SystemyWpConstants.Roles.User:
                    source = source.Where(legalAppCaseNote =>
                        context.Users.FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.ExpireDate >= DateTime.UtcNow &&
                        legalAppCaseNote.Id == noteId &&
                        legalAppCaseNote.LegalAppCaseId == caseId &&
                        legalAppCaseNote.Active == active &&
                        legalAppCaseNote.LegalAppCase.LegalAppClient.LegalAppAccessKeyId == context.Users
                            .FirstOrDefault(userEntity => userEntity.Id.Equals(userId)).LegalAppAccessKey.Id &&
                        context.DataAccesses.Any(dataAccess =>
                            dataAccess.UserId.Equals(userId) &&
                            dataAccess.RestrictedType == RestrictedType.LegalAppCase &&
                            dataAccess.ItemId == legalAppCaseNote.LegalAppCase.Id));
                    break;
            }

            return source;
        }
    }
}