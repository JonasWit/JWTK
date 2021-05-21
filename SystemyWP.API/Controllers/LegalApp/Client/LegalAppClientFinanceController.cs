using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients-finance")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppClientFinanceController : LegalAppApiController
    {
        public LegalAppClientFinanceController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/finance-records")]
        public async Task<IActionResult> GetFinanceRecords(long clientId, string from, string to)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    if (DateTime.TryParse(from, out var fromDate) &&
                        DateTime.TryParse(to, out var toDate))
                    {
                        var client = _context.LegalAppClients
                            .Include(x =>
                                x.LegalAppClientWorkRecords.Where(y =>
                                    y.Created >= fromDate && y.Created <= toDate))
                            .FirstOrDefault(x => x.Id == clientId && x.AccessKeyId == check.AccessKey.Id);

                        if (client is null) return BadRequest();

                        var result = client.LegalAppClientWorkRecords
                            .Select(LegalAppClientFinanceProjections.CreateBasic)
                            .ToList();

                        return Ok(result);
                    }
                    return BadRequest();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost("client/{clientId}/finance-records")]
        public async Task<IActionResult> CreateFinanceRecord(long clientId, [FromBody] CreateClientWorkForm createClientWorkForm)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKeyId == check.AccessKey.Id);

                        if (client is null) return BadRequest();

                        var newEntity = new LegalAppClientWorkRecord
                        {
                            Amount = createClientWorkForm.Amount,
                            Name = createClientWorkForm.Name,
                            Rate = createClientWorkForm.Rate,
                            EventDate = createClientWorkForm.EventDate,
                            UserEmail = UserEmail,
                            UserId = UserId,
                            CreatedBy = UserEmail,
                        };

                        client.LegalAppClientWorkRecords.Add(newEntity);
                        await _context.SaveChangesAsync();
                        return Ok(client);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut("client/{clientId}/finance-record/{workRecordId}")]
        public async Task<IActionResult> UpdateFinanceRecord(long clientId, long workRecordId, [FromBody] UpdateClientWorkForm updateClientWorkForm)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientWorkRecords
                        .Where(x => x.LegalAppClient.AccessKeyId == check.AccessKey.Id)
                        .FirstOrDefault(x => x.Id == workRecordId);

                    if (entity is null) return BadRequest();

                    entity.Amount = updateClientWorkForm.Amount;
                    entity.Name = updateClientWorkForm.Name;
                    entity.Rate = updateClientWorkForm.Rate;
                    entity.EventDate = updateClientWorkForm.EventDate;
                    entity.UserEmail = UserEmail;
                    entity.UserId = UserId;
                    entity.CreatedBy = UserEmail;
                    
                    await _context.SaveChangesAsync();
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
        
        [HttpDelete("client/{clientId}/finance-record/{workRecordId}")]
        public async Task<IActionResult> DeleteFinanceRecord(long clientId, long workRecordId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var entity = _context.LegalAppClientWorkRecords
                        .Where(x => x.LegalAppClient.AccessKeyId == check.AccessKey.Id)
                        .FirstOrDefault(x => x.Id == workRecordId);

                    if (entity is null) return BadRequest();
                    
                    _context.Remove(entity);
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