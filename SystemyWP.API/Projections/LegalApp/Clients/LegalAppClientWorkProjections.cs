using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients;

namespace SystemyWP.API.Projections.LegalApp.Clients
{
    public class LegalAppClientWorkProjections
    {
        public static Func<LegalAppClientWorkRecord, object> Create => Projection.Compile();
        public static Expression<Func<LegalAppClientWorkRecord, object>> Projection =>
            legalAppClientFinance => new
            {
                legalAppClientFinance.Created,
                legalAppClientFinance.CreatedBy,
                legalAppClientFinance.Id,
                legalAppClientFinance.Active,
                legalAppClientFinance.Name,
                legalAppClientFinance.LawyerName,
                legalAppClientFinance.Amount,
                legalAppClientFinance.Description,
                legalAppClientFinance.Hours,
                legalAppClientFinance.Minutes,
                legalAppClientFinance.Rate,
                legalAppClientFinance.EventDate,
                legalAppClientFinance.UserEmail,
                legalAppClientFinance.Vat
            };
    }
}