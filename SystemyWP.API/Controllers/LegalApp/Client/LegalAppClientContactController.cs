using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.GeneralApp.Contact;
using SystemyWP.API.Projections.General;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Contacts;
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

        [HttpPost("client/{clientId}/contact")]
        public async Task<IActionResult> CreateContact(int clientId, [FromBody] ContactForm contactForm)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context, true)
                    .FirstOrDefault();
                if (legalAppClient is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var newEntity = new LegalAppContactDetail
                {
                    CreatedBy = UserEmail,
                    Name = contactForm.Name,
                    Comment = contactForm.Comment,
                    Surname = contactForm.Surname,
                    Title = contactForm.Title
                };

                legalAppClient.Contacts.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("client/{clientId}/contacts")]
        public async Task<IActionResult> GetContacts(long clientId)
        {
            try
            {
                var result = _context.LegalAppContacts
                    .GetAllowedContacts(UserId, Role, clientId, _context)
                    .Select(ContactProjections.BasicProjection)
                    .ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
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
                    .AsSingleQuery()
                    .FirstOrDefault();
                if (result is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("client/{clientId}/contact/{contactId}/emails")]
        public async Task<IActionResult> CreateContactEmail(long clientId, long contactId,
            [FromBody] CreateContactEmailFrom createContactEmailFrom)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newEmail = new LegalAppEmailAddress
                {
                    CreatedBy = UserEmail,
                    Comment = createContactEmailFrom.Comment,
                    Email = createContactEmailFrom.Email
                };

                legalAppContactDetails.Emails.Add(newEmail);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}/email/{itemId}")]
        public async Task<IActionResult> DeleteContactEmail(long clientId, long contactId, long itemId)
        {
            try
            {
                var legalAppEmailAddress = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .Include(x => x.Emails.Where(y => y.Id == itemId))
                    .Select(x => x.Emails.FirstOrDefault())
                    .FirstOrDefault();
                if (legalAppEmailAddress is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.Remove(legalAppEmailAddress);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
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
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
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
                return ServerError;
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
                if (legalAppPhoneNumber is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                _context.Remove(legalAppPhoneNumber);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("client/{clientId}/contact/{contactId}/address")]
        public async Task<IActionResult> CreateContactPhysicalAddress(long clientId, long contactId,
            [FromBody] CreatePhysicalAddressForm createPhysicalAddressForm)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
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

                legalAppContactDetails.PhysicalAddresses.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
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
                if (legalAppPhysicalAddress is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                _context.Remove(legalAppPhysicalAddress);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpDelete("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> DeleteContact(long clientId, long contactId)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                _context.Remove(legalAppContactDetails);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("client/{clientId}/contact/{contactId}")]
        public async Task<IActionResult> UpdateContact(long clientId, long contactId,
            [FromBody] ContactForm updateContactForm)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                legalAppContactDetails.Name = updateContactForm.Name;
                legalAppContactDetails.Comment = updateContactForm.Comment;
                legalAppContactDetails.Surname = updateContactForm.Surname;
                legalAppContactDetails.Title = updateContactForm.Title;

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