using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients-work")]
    [Authorize(SystemyWpConstants.Policies.User)]
    public class LegalAppClientWorkController : LegalAppApiController
    {
        public LegalAppClientWorkController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/work-records")]
        public async Task<IActionResult> GetWorkRecords(long clientId, string from, string to)
        {
            try
            {
                if (DateTime.TryParse(from, out var fromDate) &&
                    DateTime.TryParse(to, out var toDate))
                {
                    var result = _context.LegalAppClientWorkRecords
                        .GetAllowedWorkRecords(UserId, Role, clientId, fromDate, toDate, _context)
                        .Select(LegalAppClientWorkProjections.Projection)
                        .ToList();
                    
                    return Ok(result);  
                }
                
                return BadRequest("Date parameters incorrect or missing!");
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("client/{clientId}/work-record")]
        public async Task<IActionResult> CreateWorkRecord(long clientId, [FromBody] ClientWorkForm form)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();
                if (client is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newEntity = new LegalAppClientWorkRecord
                {
                    LawyerName = form.LawyerName,
                    Description = form.Description,
                    Amount = form.Amount,
                    Name = form.Name,
                    Rate = form.Rate,
                    Hours = form.Hours,
                    Minutes = form.Minutes,
                    EventDate = form.EventDate,
                    Vat = form.Vat,
                    UserId = UserId,
                    CreatedBy = UserEmail,
                };

                client.LegalAppClientWorkRecords.Add(newEntity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("client/{clientId}/work-record/{workRecordId}")]
        public async Task<IActionResult> UpdateWorkRecord(long clientId, long workRecordId, [FromBody] ClientWorkForm form)
        {
            try
            {
                var legalAppClientWorkRecord = _context.LegalAppClientWorkRecords
                        .GetAllowedWorkRecord(UserId, Role, clientId, workRecordId, _context)
                        .FirstOrDefault();
                if (legalAppClientWorkRecord is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                legalAppClientWorkRecord.LawyerName = form.LawyerName;
                legalAppClientWorkRecord.Amount = form.Amount;
                legalAppClientWorkRecord.Name = form.Name;
                legalAppClientWorkRecord.Rate = form.Rate;
                legalAppClientWorkRecord.EventDate = form.EventDate;
                legalAppClientWorkRecord.UserId = UserId;
                legalAppClientWorkRecord.CreatedBy = UserEmail;
                legalAppClientWorkRecord.Vat = form.Vat;

                await _context.SaveChangesAsync();
                return Ok(legalAppClientWorkRecord);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpDelete("client/{clientId}/work-record/{workRecordId}")]
        public async Task<IActionResult> DeleteWorkRecord(long clientId, long workRecordId)
        {
            try
            {
                var legalAppClientWorkRecord = _context.LegalAppClientWorkRecords
                    .GetAllowedWorkRecord(UserId, Role, clientId, workRecordId, _context)
                    .FirstOrDefault();
                if (legalAppClientWorkRecord is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);     
                
                _context.Remove(legalAppClientWorkRecord);
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