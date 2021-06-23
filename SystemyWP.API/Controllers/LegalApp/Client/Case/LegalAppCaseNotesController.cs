using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.Forms.GeneralApp.Note;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Client.Case
{
    [Route("/api/legal-app-cases-notes")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppCaseNotesController : LegalAppApiController
    {
        public LegalAppCaseNotesController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }
        
        [HttpGet("case/{caseId}/list")]
        public async Task<IActionResult> GetNotes(long caseId)
        {
            try
            {
                var result = _context.LegalAppCaseNotes
                    .GetAllowedNotes(UserId, Role, caseId, _context)
                    .Select(LegalAppCaseNoteProjections.BasicProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("case/{caseId}/note/{noteId}")]
        public async Task<IActionResult> GetNote(long caseId, long noteId)
        {
            try
            {
                var note = _context.LegalAppCaseNotes
                    .GetAllowedNote(UserId, Role, caseId, noteId, _context)
                    .Select(LegalAppCaseNoteProjections.Projection)
                    .FirstOrDefault();
                if (note is null) return BadRequest("Note not found");
                
                return Ok(note);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("case/{caseId}")]
        public async Task<IActionResult> CreateNote(long caseId, [FromBody] NoteForm form)
        {
            try
            {
                var legalAppCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context, true)
                    .FirstOrDefault();
                if (legalAppCase is null) return BadRequest();

                legalAppCase.LegalAppCaseNotes.Add(new LegalAppCaseNote()
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

        [HttpDelete("case/{caseId}/note/{noteId}")]
        public async Task<IActionResult> DeleteNote(long caseId, long noteId, [FromBody] NoteForm form)
        {
            try
            {
                var legalAppCaseNote = _context.LegalAppCaseNotes
                    .GetAllowedNote(UserId, Role, caseId, noteId, _context)
                    .FirstOrDefault();
                if (legalAppCaseNote is null) return BadRequest("Note not found");

                _context.Remove(legalAppCaseNote);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("case/{caseId}/note/{noteId}")]
        public async Task<IActionResult> UpdateNote(long caseId, long noteId, [FromBody] NoteForm form)
        {
            try
            {
                var legalAppCaseNote = _context.LegalAppCaseNotes
                    .GetAllowedNote(UserId, Role, caseId, noteId, _context)
                    .FirstOrDefault();
                if (legalAppCaseNote is null) return BadRequest("Note not found");

                legalAppCaseNote.UpdatedBy = UserEmail;
                legalAppCaseNote.Updated = DateTime.UtcNow;
                legalAppCaseNote.Message = form.Message;
                legalAppCaseNote.Title = form.Title;

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