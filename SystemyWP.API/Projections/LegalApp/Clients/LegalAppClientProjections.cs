﻿using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Clients
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
    }
}