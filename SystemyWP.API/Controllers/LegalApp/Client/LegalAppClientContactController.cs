using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.GeneralApp.Contact;
using SystemyWP.API.Projections.General;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.LegalAppModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
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
                var result = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context, true)
                    .FirstOrDefault();
                if (result is null) return BadRequest();

                var newEntity = new LegalAppContactDetails
                {
                    CreatedBy = UserEmail,
                    Name = createContactForm.Name,
                    Comment = createContactForm.Comment,
                    Surname = createContactForm.Surname,
                    Title = createContactForm.Title
                };

                result.Contacts.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("client/{clientId}/contacts")]
        public async Task<IActionResult> GetContacts(long clientId)
        {
            try
            {
                var result = _context.LegalAppContacts
                    .GetAllowedContacts(UserId, Role, clientId, _context)
                    .Select(ContactProjections.FullProjection)
                    .ToList();
                
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> GetContact(long clientId, long contactId)
        {
            try
            {
                var result = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .Select(ContactProjections.FullProjection)
                    .FirstOrDefault();       
                
                if (result is null) return BadRequest();
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/contact/{contactId}/emails")]
        public async Task<IActionResult> CreateContactEmail(long clientId, long contactId,
            [FromBody] CreateContactEmailFrom createContactEmailFrom)
        {
            try
            {
                var result = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();       
                
                if (result is null) return BadRequest();
                
                var newEmail = new LegalAppEmailAddress
                {
                    CreatedBy = UserEmail,
                    Comment = createContactEmailFrom.Comment,
                    Email = createContactEmailFrom.Email
                };

                result.Emails.Add(newEmail);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}/email/{itemId}")]
        public async Task<IActionResult> DeleteContactEmail(long clientId, long contactId, long itemId)
        {
            try
            {
                var entityToRemove = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .Include(x => x.Emails.Where(y => y.Id == itemId))
                    .Select(x => x.Emails.FirstOrDefault())
                    .FirstOrDefault();       
                
                if (entityToRemove is null) return BadRequest();

                _context.Remove(entityToRemove);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/contact/{contactId}/phone-number")]
        public async Task<IActionResult> CreateContactPhoneNumber(long clientId, long contactId,
            [FromBody] CreateContactPhoneNumberForm createContactPhoneNumberForm)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();   
                
                if (legalAppContactDetails is null) return BadRequest(); 
                
                var newPhone = new LegalAppPhoneNumber()
                {
                    CreatedBy = UserEmail,
                    Number = createContactPhoneNumberForm.Number,
                    Comment = createContactPhoneNumberForm.Comment
                };

                legalAppContactDetails.PhoneNumbers.Add(newPhone);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}/phone-number/{itemId}")]
        public async Task<IActionResult> DeleteContactPhoneNumber(long clientId, long contactId, long itemId)
        {
            try
            {
                var legalAppPhoneNumber = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .Include(x => x.PhoneNumbers.Where(y => y.Id == itemId))
                    .Select(x => x.PhoneNumbers.FirstOrDefault())
                    .FirstOrDefault();       
                
                if (legalAppPhoneNumber is null) return BadRequest(); 
                
                _context.Remove(legalAppPhoneNumber);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/contact/{contactId}/address")]
        public async Task<IActionResult> CreateContactPhysicalAddress(long clientId, long contactId,
            [FromBody] CreatePhysicalAddressForm createPhysicalAddressForm)
        {
            try
            {
                var contact = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();   
                
                if (contact is null) return BadRequest(); 
                
                var newEntity = new LegalAppPhysicalAddress()
                {
                    CreatedBy = UserEmail,
                    Building = createPhysicalAddressForm.Building,
                    Comment = createPhysicalAddressForm.Comment,
                    Country = createPhysicalAddressForm.Country,
                    City = createPhysicalAddressForm.City,
                    Street = createPhysicalAddressForm.Street,
                    PostCode = createPhysicalAddressForm.PostCode
                };

                contact.PhysicalAddresses.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}/address/{itemId}")]
        public async Task<IActionResult> DeleteContactPhysicalAddress(long clientId, long contactId, long itemId)
        {
            try
            {
                var legalAppPhysicalAddress = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .Include(x => x.PhysicalAddresses.Where(y => y.Id == itemId))
                    .Select(x => x.PhysicalAddresses.FirstOrDefault())
                    .FirstOrDefault();       
                
                if (legalAppPhysicalAddress is null) return BadRequest();
                
                _context.Remove(legalAppPhysicalAddress);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> DeleteContact(long clientId, long contactId)
        {
            try
            {
                var contact = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();       
                
                if (contact is null) return BadRequest();
                
                _context.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok();
 
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var contact = _context.LegalAppContacts
                //         .Where(x => x.LegalAppClientId == _context.LegalAppClients
                //                         .FirstOrDefault(y =>
                //                             y.Id == clientId && y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
                //                         .Id &&
                //                     x.Id == contactId)
                //         .FirstOrDefault();
                //
                //     if (contact is null) return BadRequest();
                //
                //     _context.Remove(contact);
                //     await _context.SaveChangesAsync();
                //     return Ok();
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
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
                var contact = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();       
                
                if (contact is null) return BadRequest();
                
                contact.Name = updateContactForm.Name;
                contact.Comment = updateContactForm.Comment;
                contact.Surname = updateContactForm.Surname;
                contact.Title = updateContactForm.Title;

                await _context.SaveChangesAsync();
                return Ok();  
                
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var contact = _context.LegalAppContacts
                //         .Where(x => x.LegalAppClientId == _context.LegalAppClients
                //                         .FirstOrDefault(y =>
                //                             y.Id == clientId && y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
                //                         .Id &&
                //                     x.Id == contactId)
                //         .FirstOrDefault();
                //
                //     if (contact is null) return BadRequest();
                //
                //     contact.Name = updateContactForm.Name;
                //     contact.Comment = updateContactForm.Comment;
                //     contact.Surname = updateContactForm.Surname;
                //     contact.Title = updateContactForm.Title;
                //
                //     await _context.SaveChangesAsync();
                //     return Ok();
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
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