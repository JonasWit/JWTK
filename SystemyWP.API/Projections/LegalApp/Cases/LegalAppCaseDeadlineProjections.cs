using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;

namespace SystemyWP.API.Projections.LegalApp.Cases
{
    public class LegalAppCaseDeadlineProjections
    {
        public static Func<LegalAppCaseDeadline, object> CreateBasic => Projection.Compile();
        public static Expression<Func<LegalAppCaseDeadline, object>> Projection =>
            legalAppCaseDeadline => new
            {
                legalAppCaseDeadline.Id,
                legalAppCaseDeadline.Created,
                legalAppCaseDeadline.CreatedBy,
                legalAppCaseDeadline.Deadline, 
                legalAppCaseDeadline.Message, 
            };
    }
}