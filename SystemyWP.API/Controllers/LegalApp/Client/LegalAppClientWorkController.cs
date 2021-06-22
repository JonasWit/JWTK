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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients-work")]
    [Authorize(SystemyWpConstants.Policies.Client)]
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
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                
                if (client is null) return BadRequest("Client not found");
                
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("client/{clientId}/work-record/{workRecordId}")]
        public async Task<IActionResult> UpdateWorkRecord(long clientId, long workRecordId,
            [FromBody] ClientWorkForm form)
        {
            try
            {
                var entity = _context.LegalAppClientWorkRecords
                        .GetAllowedWorkRecord(UserId, Role, clientId, workRecordId, _context)
                        .FirstOrDefault();
                
                if (entity is null) return BadRequest("Record not found");
                
                entity.LawyerName = form.LawyerName;
                entity.Amount = form.Amount;
                entity.Name = form.Name;
                entity.Rate = form.Rate;
                entity.EventDate = form.EventDate;
                entity.UserId = UserId;
                entity.CreatedBy = UserEmail;
                entity.Vat = form.Vat;

                await _context.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("client/{clientId}/work-record/{workRecordId}")]
        public async Task<IActionResult> DeleteWorkRecord(long clientId, long workRecordId)
        {
            try
            {
                var entity = _context.LegalAppClientWorkRecords
                    .GetAllowedWorkRecord(UserId, Role, clientId, workRecordId, _context)
                    .FirstOrDefault();
                
                if (entity is null) return BadRequest("Work record or client not found");           
                
                _context.Remove(entity);
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