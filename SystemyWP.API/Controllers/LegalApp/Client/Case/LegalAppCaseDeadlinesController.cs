﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.LegalApp.Client.Case;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
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
        
        [HttpGet("case/{caseId}/list")]
        public async Task<IActionResult> GetDeadlines(long caseId, string from, string to)
        {
            try
            {
                if (DateTime.TryParse(from, out var fromDate) &&
                    DateTime.TryParse(to, out var toDate))
                {
                    var result = _context.LegalAppCaseDeadlines
                        .GetAllowedDeadlines(UserId, Role, caseId, fromDate, toDate, _context)
                        .Select(LegalAppCaseDeadlineProjections.Projection)
                        .ToList();

                    return Ok(result);
                }
                
                return BadRequest();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }    

        [HttpPost("case/{caseId}/create")]
        public async Task<IActionResult> CreateDeadline(long caseId, [FromBody] DeadlineFrom form)
        {
            try
            {
                var legalAppCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context, true)
                    .FirstOrDefault();
                if (legalAppCase is null) return BadRequest("Case not found");

                legalAppCase.LegalAppCaseDeadlines.Add(new LegalAppCaseDeadline
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

        [HttpDelete("case/{caseId}/item/{deadlineId}/delete")]
        public async Task<IActionResult> DeleteDeadline(long caseId, long deadlineId)
        {
            try
            {
                var deadlineToDelete = _context.LegalAppCaseDeadlines
                    .GetAllowedDeadline(UserId, Role, caseId, deadlineId, _context)
                    .FirstOrDefault();
                if (deadlineToDelete is null) return BadRequest("Deadline not found");

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