using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp.Contact;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Projections.LegalApp.Contacts;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-client-contacts")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppClientContactController : LegalAppApiController
    {
        public LegalAppClientContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpPost("client/{clientId}/contact/create")]
        public async Task<IActionResult> CreateContact(int clientId, [FromBody] CreateContactForm createContactForm)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Include(x => x.Contacts)
                        .FirstOrDefault();

                    if (result is null) return BadRequest();

                        result.Contacts.Add(new ContactDetails
                    {
                        Name = createContactForm.Name,
                        Comment = createContactForm.Comment
                    });
                    
                    return Ok(result);
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
        
        [HttpGet("client/{clientId}/contacts")]
        public async Task<IActionResult> GetContacts(int clientId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.Emails)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhoneNumbers)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhysicalAddresses)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Select(x => LegalAppContactProjections.BasicProjection)
                        .ToList();
                    
                    return Ok(result);
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
        public async Task<IActionResult> DeleteContact(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Include(x => x.Contacts.FirstOrDefault(y => y.Id == contactId))
                        .FirstOrDefault();
                    
                    if (result is null || result.Contacts.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError);

                    _context.Remove(result.Contacts.First());
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
        
        
        
        
        
        
        
    }
}