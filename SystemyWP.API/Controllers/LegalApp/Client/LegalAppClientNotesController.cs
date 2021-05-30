using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entities = _context.LegalAppClientNotes
                        .Where(x => x.LegalAppClientId == _context.LegalAppClients
                            .FirstOrDefault(y =>
                                y.Id == clientId && y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id)
                        .Select(LegalAppClientNoteProjections.BasicProjection)
                        .ToList();

                    return Ok(entities);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
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
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entities = _context.LegalAppClientNotes
                        .Where(x => x.LegalAppClientId == _context.LegalAppClients
                            .FirstOrDefault(y =>
                                y.Id == clientId && y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id)
                        .OrderBy(x => x.Title)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppClientNoteProjections.BasicProjection)
                        .ToList();

                    return Ok(entities);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
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
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientNotes
                        .Where(x => x.Id == noteId &&
                                    x.LegalAppClientId == _context.LegalAppClients
                                        .FirstOrDefault(y =>
                                            y.Id == clientId && y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id)
                        .Select(LegalAppClientNoteProjections.BasicProjection)
                        .FirstOrDefault();

                    return Ok(entity);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/create-note")]
        public async Task<IActionResult> CreateNote(int clientId, [FromBody] ClientNoteForm form)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Where(x => x.LegalAppAccessKeyId == check.LegalAppAccessKey.Id && x.Id == clientId)
                        .Include(x => x.Contacts)
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

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("client/{clientId}/note/{noteId}")]
        public async Task<IActionResult> UpdateNote(long clientId, long noteId, [FromBody] ClientNoteForm form)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientNotes
                        .Where(x => x.LegalAppClientId == _context.LegalAppClients
                            .FirstOrDefault(y => y.Id == clientId &&
                            y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id &&
                            x.Id == noteId)
                        .FirstOrDefault();

                    if (entity is null) return BadRequest();

                    entity.Title = form.Title;
                    entity.Message = form.Message;

                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> DeleteNote(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientNotes
                        .Where(x => x.LegalAppClientId == _context.LegalAppClients
                            .FirstOrDefault(y => y.Id == clientId &&
                            y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id && x.Id == contactId)
                        .FirstOrDefault();

                    if (entity is null) return BadRequest();

                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("archive/{clientId}/note/{noteId}")]
        public async Task<IActionResult> ArchiveNote(long clientId, long noteId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientNotes
                        .Where(x => x.LegalAppClientId == _context.LegalAppClients
                            .FirstOrDefault(y => y.Id == clientId &&
                                y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id &&
                                x.Id == noteId)
                        .FirstOrDefault();

                    if (entity is null) return BadRequest();

                    entity.Active = !entity.Active;

                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}