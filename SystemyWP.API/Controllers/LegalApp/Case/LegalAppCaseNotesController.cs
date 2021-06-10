using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.GeneralApp.Note;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Case
{
    [Route("/api/legal-app-cases-notes")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppCaseNotesController : LegalAppApiController
    {
        public LegalAppCaseNotesController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/case/{caseId}/note/{noteId}")]
        public async Task<IActionResult> GetNote(long clientId, long caseId, long noteId)
        {
            try
            {
                var note = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .Include(x => x.LegalAppCaseNotes
                        .Where(y => y.Id == noteId))
                    .Select(x => x.LegalAppCaseNotes.FirstOrDefault())
                    .Select(LegalAppCaseNoteProjections.Projection)
                    .FirstOrDefault();
                if (note is null) return BadRequest();
                
                return Ok(note);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/case/{caseId}/create")]
        public async Task<IActionResult> CreateNote(long clientId, long caseId, [FromBody] NoteForm form)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .FirstOrDefault();
                if (result is null) return BadRequest();

                result.LegalAppCaseNotes.Add(new LegalAppCaseNote()
                {
                    Title = form.Title,
                    Message = form.Message,
                    CreatedBy = UserEmail,
                    UpdatedBy = UserEmail
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

        [HttpPost("client/{clientId}/case/{caseId}/note/{noteId}/delete")]
        public async Task<IActionResult> DeleteNote(long clientId, long caseId, long noteId, [FromBody] NoteForm form)
        {
            try
            {
                var note = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .Include(x => x.LegalAppCaseNotes.Where(y => y.Id == noteId))
                    .Select(x => x.LegalAppCaseNotes.FirstOrDefault())
                    .AsSingleQuery()
                    .FirstOrDefault();

                if (note is null) return BadRequest();

                _context.Remove(note);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/case/{caseId}/note/{noteId}/update")]
        public async Task<IActionResult> UpdateNote(long clientId, long caseId, long noteId, [FromBody] NoteForm form)
        {
            try
            {
                var note = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .Include(x => x.LegalAppCaseNotes.Where(y => y.Id == noteId))
                    .Select(x => x.LegalAppCaseNotes.FirstOrDefault())
                    .AsSingleQuery()
                    .FirstOrDefault();

                if (note is null) return BadRequest();

                note.UpdatedBy = UserEmail;
                note.Updated = DateTime.UtcNow;
                note.Message = form.Message;
                note.Title = form.Title;

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