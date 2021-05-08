﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Systemywp.Data.Models.LegalAppModels.Clients;

namespace Systemywp.Api.Projections.LegalApp
{
    public class LegalAppClientProjections
    {
        public static Func<LegalAppClient, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> FlatProjection =>
            legalAppClient => new
            {
                legalAppClient.Created,
                legalAppClient.CreatedBy,
                legalAppClient.Updated,
                legalAppClient.UpdatedBy,
                legalAppClient.Id,
                legalAppClient.Active,
                legalAppClient.Name,
            };

        public static Func<LegalAppClient, object> CreateBasic => BasicProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> BasicProjection =>
            legalAppClient => new
            {
                legalAppClient.Id,
                legalAppClient.Name,
            };

        public static Func<LegalAppClient, object> CreateMinimal => MinimalProjection.Compile();
        public static Expression<Func<LegalAppClient, object>> MinimalProjection =>
            legalAppClient => new
            {
                legalAppClient.Id,
                legalAppClient.Name,
                Cases = legalAppClient.LegalAppCases
                    .AsQueryable()
                    .Where(x => x.Active)
                    .Select(LegalAppCaseProjections.MinimalProjection)
                    .ToList()
            };
    }
}