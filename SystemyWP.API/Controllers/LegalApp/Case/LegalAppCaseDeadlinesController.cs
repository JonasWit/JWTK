using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.LegalApp.Case;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Case
{
    [Route("/api/legal-app-cases/deadlines")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppCaseDeadlinesController : LegalAppApiController
    {
        public LegalAppCaseDeadlinesController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpPost("/client/{clientId}/case/{caseId}/deadline/create")]
        public async Task<IActionResult> CreateDeadline(long clientId, long caseId, [FromBody] DeadlineFrom form)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .FirstOrDefault();
                if (result is null) return BadRequest();

                result.LegalAppCaseDeadlines.Add(new LegalAppCaseDeadline
                {
                    Deadline = form.Deadline,
                    Message = form.Message,
                    CreatedBy = UserEmail
                });

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("/client/{clientId}/case/{caseId}/deadline/{deadlineId}/delete")]
        public async Task<IActionResult> DeleteDeadline(long clientId, long caseId, long deadlineId)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .FirstOrDefault();
                if (result is null) return BadRequest();
                
                var deadlineToDelete = result.LegalAppCaseDeadlines
                    .FirstOrDefault(x => x.Id == deadlineId);
                if (deadlineToDelete is null) return BadRequest();

                _context.Remove(deadlineToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}