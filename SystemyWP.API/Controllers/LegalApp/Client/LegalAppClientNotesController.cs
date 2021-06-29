using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.GeneralApp.Note;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients-notes")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class LegalAppClientNotesController : LegalAppApiController
    {
        public LegalAppClientNotesController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/notes/titles-list")]
        public async Task<IActionResult> GetNotesTitlesList(long clientId)
        {
            try
            {
                var notes = _context.LegalAppClientNotes
                    .GetAllowedNotes(UserId, Role, clientId, _context)
                    .Select(LegalAppClientNoteProjections.BasicProjection)   
                    .ToList();
                
                return Ok(notes);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("client/{clientId}/note/{noteId}")]
        public async Task<IActionResult> GetNote(long clientId, long noteId)
        {
            try
            {
                var notes = _context.LegalAppClientNotes
                    .GetAllowedNote(UserId, Role, clientId, noteId, _context)
                    .Select(LegalAppClientNoteProjections.Projection)   
                    .FirstOrDefault();        
                
                return Ok(notes);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("client/{clientId}/note")]
        public async Task<IActionResult> CreateNote(int clientId, [FromBody] NoteForm form)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var newEntity = new LegalAppClientNote
                {
                    Public = form.Public,
                    AuthorId = UserId,
                    CreatedBy = UserEmail,
                    Title = form.Title,
                    Message = form.Message,
                    UpdatedBy = UserEmail,
                    Updated = DateTime.UtcNow
                };

                client.LegalAppClientNotes.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("client/{clientId}/note/{noteId}")]
        public async Task<IActionResult> UpdateNote(long clientId, long noteId, [FromBody] NoteForm form)
        {
            try
            {
                var note = _context.LegalAppClientNotes
                    .GetAllowedNote(UserId, Role, clientId, noteId, _context)
                    .FirstOrDefault();
                if (note is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                note.Public = form.Public;
                note.Title = form.Title;
                note.Message = form.Message;
                note.UpdatedBy = UserEmail;
                note.Updated = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpDelete("client/{clientId}/note/{noteId}")]
        public async Task<IActionResult> DeleteNote(long clientId, long noteId)
        {
            try
            {
                var note = _context.LegalAppClientNotes
                    .GetAllowedNote(UserId, Role, clientId, noteId, _context)
                    .FirstOrDefault();
                if (note is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                _context.Remove(note);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}