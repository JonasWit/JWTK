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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-client-contacts")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class LegalAppClientContactController : LegalAppApiController
    {
        public LegalAppClientContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpPost("client/{clientId}/contact")]
        public async Task<IActionResult> CreateContact(int clientId, [FromBody] ContactForm form)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context, true)
                    .FirstOrDefault();
                if (legalAppClient is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var newEntity = new LegalAppContactDetail
                {
                    CreatedBy = Username,
                    Name = form.Name,
                    Comment = form.Comment,
                    Surname = form.Surname,
                    Title = form.Title
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
            [FromBody] CreateContactEmailFrom form)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newEmail = new LegalAppEmailAddress
                {
                    CreatedBy = Username,
                    Comment = form.Comment,
                    Email = form.Email
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
            [FromBody] CreateContactPhoneNumberForm form)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newPhone = new LegalAppPhoneNumber()
                {
                    CreatedBy = Username,
                    Number = form.Number,
                    Comment = form.Comment
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
            [FromBody] CreatePhysicalAddressForm form)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newEntity = new LegalAppPhysicalAddress()
                {
                    CreatedBy = Username,
                    Building = form.Building,
                    Comment = form.Comment,
                    Country = form.Country,
                    City = form.City,
                    Street = form.Street,
                    PostCode = form.PostCode
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
            [FromBody] ContactForm form)
        {
            try
            {
                var legalAppContactDetails = _context.LegalAppContacts
                    .GetAllowedContact(UserId, Role, clientId, contactId, _context)
                    .FirstOrDefault();
                if (legalAppContactDetails is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                legalAppContactDetails.Name = form.Name;
                legalAppContactDetails.Comment = form.Comment;
                legalAppContactDetails.Surname = form.Surname;
                legalAppContactDetails.Title = form.Title;

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