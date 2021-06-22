using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.LegalApp.Case;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client.Case
{
    [Route("/api/legal-app-cases/deadlines")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppCaseDeadlinesController : LegalAppApiController
    {
        public LegalAppCaseDeadlinesController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpPost("client/{clientId}/case/{caseId}/create")]
        public async Task<IActionResult> CreateDeadline(long clientId, long caseId, [FromBody] DeadlineFrom form)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
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

        [HttpDelete("client/{clientId}/case/{caseId}/item/{deadlineId}/delete")]
        public async Task<IActionResult> DeleteDeadline(long clientId, long caseId, long deadlineId)
        {
            try
            {
                var deadlineToDelete = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .Include(x => x.LegalAppCaseDeadlines.Where(y => y.Id == deadlineId))
                    .Select(x => x.LegalAppCaseDeadlines.FirstOrDefault())
                    .AsSingleQuery()
                    .FirstOrDefault();
                
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