using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.GeneralApp.Contact;
using SystemyWP.API.Projections.General;
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
    public class LegalAppClientContactController : LegalAppApiController
    {
        public LegalAppClientContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
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

                    var newContact = new ContactDetails
                    {
                        CreatedBy = UserEmail,
                        Name = createContactForm.Name,
                        Comment = createContactForm.Comment
                    };

                    result.Contacts.Add(newContact);
                    await _context.SaveChangesAsync();
                    return Ok(newContact);
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
        public async Task<IActionResult> GetContacts(long clientId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null) return BadRequest();

                    var result = client.Contacts
                        .Select(ContactProjections.CreateBasic)
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

        [HttpGet("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> GetContact(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    return Ok(client.Contacts.First());
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
        
        [HttpGet("client/{clientId}/contact/{contactId}/emails")]
        public async Task<IActionResult> GetContactEmails(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                            .ThenInclude(contact => contact.Emails)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    return Ok(client.Contacts.First().Emails.AsQueryable().Select(EmailProjection.FullProjection).ToList());
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
        
        [HttpPost("client/{clientId}/contact/{contactId}/emails")]
        public async Task<IActionResult> CreateContactEmail(long clientId, long contactId, [FromBody] CreateContactEmailFrom createContactEmailFrom)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    var contact = client.Contacts.FirstOrDefault(x => x.Id == contactId);
                    if (contact is null) return BadRequest();
                    
                    var newEmail = new EmailAddress
                    {
                        CreatedBy = UserEmail,
                        Comment = createContactEmailFrom.Comment,
                        Email = createContactEmailFrom.Email
                    };
                    
                    contact.Emails.Add(newEmail);
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
        
        [HttpDelete("client/{clientId}/contact/{contactId}/email/{emailId}")]
        public async Task<IActionResult> DeleteContactEmail(long clientId, long contactId, long emailId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                            .ThenInclude(y => y.Emails)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    var contact = client.Contacts.FirstOrDefault(x => x.Id == contactId);
                    if (contact is null) return BadRequest();

                    contact.Emails.RemoveAll(x => x.Id == emailId);
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
        
        [HttpGet("client/{clientId}/contact/{contactId}/phone-numbers")]
        public async Task<IActionResult> GetContactPhoneNumbers(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                            .ThenInclude(contact => contact.PhoneNumbers)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    return Ok(client.Contacts.First());
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
        
        [HttpGet("client/{clientId}/contact/{contactId}/addresses")]
        public async Task<IActionResult> GetContactAddresses(long clientId, long contactId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                            .ThenInclude(contact => contact.PhysicalAddresses)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    return Ok(client.Contacts.First());
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
                        .Include(x => x.Contacts.Where(y => y.Id == contactId))
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .FirstOrDefault();

                    if (result is null || result.Contacts.Count == 0)
                        return BadRequest();

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

        [HttpPut("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> UpdateContact(long clientId, long contactId,
            [FromBody] UpdateContactForm updateContactForm)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Include(x => x.Contacts.FirstOrDefault(y => y.Id == contactId))
                        .FirstOrDefault();

                    if (client is null || client.Contacts.Count == 0) return BadRequest();

                    var contact = client.Contacts.FirstOrDefault(x => x.Id == contactId);
                    if (contact is null) return BadRequest();

                    contact.Name = updateContactForm.Name;
                    contact.Comment = updateContactForm.Comment;

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