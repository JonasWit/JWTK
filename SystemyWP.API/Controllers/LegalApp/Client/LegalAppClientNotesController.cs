﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.GeneralApp.Note;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients-notes")]
    [Authorize(SystemyWpConstants.Policies.Client)]
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
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entities = _context.LegalAppClientNotes
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClientId == _context.LegalAppClients
                //                 .FirstOrDefault(y =>
                //                     y.Id == clientId &&
                //                     y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id)
                //         .Select(LegalAppClientNoteProjections.BasicProjection)
                //         .ToList();
                //
                //     return Ok(entities);
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("client/{clientId}/notes")]
        public async Task<IActionResult> GetNotes(long clientId, int cursor, int take)
        {
            try
            {
                var notes = _context.LegalAppClientNotes
                    .GetAllowedNotes(UserId, Role, clientId, _context)
                    .OrderBy(x => x.Title)
                    .Skip(cursor)
                    .Take(take)
                    .Select(LegalAppClientNoteProjections.FullProjection)   
                    .ToList();
                
                return Ok(notes);
                
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entities = _context.LegalAppClientNotes
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClientId == _context.LegalAppClients
                //                 .FirstOrDefault(y =>
                //                     y.Id == clientId &&
                //                     y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id)
                //         .OrderBy(x => x.Title)
                //         .Skip(cursor)
                //         .Take(take)
                //         .Select(LegalAppClientNoteProjections.BasicProjection)
                //         .ToList();
                //
                //     return Ok(entities);
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("client/{clientId}/notes/{noteId}")]
        public async Task<IActionResult> GetNote(long clientId, long noteId)
        {
            try
            {
                var notes = _context.LegalAppClientNotes
                    .GetAllowedNote(UserId, Role, clientId, noteId, _context)
                    .Select(LegalAppClientNoteProjections.FullProjection)   
                    .FirstOrDefault();        
                
                return Ok(notes);
                
                //
                // var entity = _context.LegalAppClientNotes
                //     .Where(x =>
                //         x.Active &&
                //         x.Id == noteId &&
                //         x.LegalAppClientId == _context.LegalAppClients
                //             .GetAllowedClient(UserId, Role, clientId, _context, true)
                //             .FirstOrDefault().Id)
                //     .Select(LegalAppClientNoteProjections.BasicProjection)
                //     .FirstOrDefault();

            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/create-note")]
        public async Task<IActionResult> CreateNote(int clientId, [FromBody] NoteForm form)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();

                if (client is null) return BadRequest();

                var newEntity = new LegalAppClientNote
                {
                    CreatedBy = UserEmail,
                    Title = form.Title,
                    Message = form.Message
                };

                client.LegalAppClientNotes.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok(newEntity);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
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

                if (note is null) return BadRequest();

                note.Title = form.Title;
                note.Message = form.Message;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
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

                if (note is null) return BadRequest();
                
                _context.Remove(note);
                await _context.SaveChangesAsync();
                return Ok();
                
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entity = _context.LegalAppClientNotes
                //         .Where(x =>
                //             x.LegalAppClientId == _context.LegalAppClients
                //                 .FirstOrDefault(y =>
                //                     y.Id == clientId &&
                //                     y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id &&
                //             x.Id == noteId)
                //         .FirstOrDefault();
                //
                //     if (entity is null) return BadRequest();
                //
                //     _context.Remove(entity);
                //     await _context.SaveChangesAsync();
                //     return Ok();
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}