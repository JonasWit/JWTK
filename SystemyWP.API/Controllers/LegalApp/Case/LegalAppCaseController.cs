using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.LegalApp.Case;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Case
{
    [Route("/api/legal-app-cases")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppCaseController : LegalAppApiController
    {
        public LegalAppCaseController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("client/{clientId}/cases")]
        public async Task<IActionResult> GetCases(long clientId)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCases(UserId, Role, clientId, _context)
                    .Select(LegalAppCaseProjections.Projection)
                    .ToList();

                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                //     Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                // {
                //     var cases = _context.LegalAppCases
                //         .Include(x => x.LegalAppClient)
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClient.Id == clientId &&
                //             x.LegalAppClient.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
                //         .Select(LegalAppCaseProjections.Projection)
                //         .ToList();
                //
                //     return Ok(cases);
                // }
                //
                // //Get data as User
                // if (Role.Equals(SystemyWpConstants.Roles.Client))
                // {
                //     if (check.DataAccessAllowed)
                //     {
                //         var cases = _context.LegalAppCases
                //             .Include(x => x.LegalAppClient)
                //             .Where(x =>
                //                 x.Active &&
                //                 x.LegalAppClient.Id == clientId &&
                //                 x.LegalAppClient.LegalAppAccessKeyId == check.LegalAppAccessKey.Id &&
                //                 _context.DataAccesses
                //                     .Where(y =>
                //                         y.UserId.Equals(UserId) &&
                //                         y.RestrictedType == RestrictedType.LegalAppCase)
                //                     .Any(y => y.ItemId == x.Id))
                //             .Select(LegalAppCaseProjections.Projection)
                //             .ToList();
                //
                //         return Ok(cases);
                //     }
                // }
                //
                return Ok(result);

                //return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("client/{clientId}/case/{caseId}")]
        public async Task<IActionResult> GetCase(long clientId, long caseId)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .Include(x => x.LegalAppCaseDeadlines)
                    .Select(LegalAppCaseProjections.Projection)
                    .FirstOrDefault();
                
                // var check = await CheckAccess(RestrictedType.LegalAppCase, caseId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entity = _context.LegalAppCases
                //         .Include(x => x.LegalAppClient)
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClient.Id == clientId &&
                //             x.LegalAppClient.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
                //         .Select(LegalAppCaseProjections.Projection)
                //         .FirstOrDefault();
                //
                //     return Ok(entity);
                // }
                //
                // return StatusCode(StatusCodes.Status403Forbidden);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("client/{clientId}/case/create")]
        public async Task<IActionResult> CreateCase(long clientId, [FromBody] CaseForm form)
        {
            try
            {
                // var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                
                var appClient = _context.LegalAppClients
                    .GetAllowedClients(UserId, Role, _context)
                    .FirstOrDefault();
                if (appClient is null) return BadRequest();
                
                var newCase = new LegalAppCase()
                {
                    Group = form.Group,
                    Description = form.Description,
                    Signature = form.Signature,
                    Name = form.Name,
                    CreatedBy = UserEmail,
                    UpdatedBy = UserEmail
                };
                
                appClient.LegalAppCases.Add(newCase);
                await _context.SaveChangesAsync();

                //Act as normal as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    _context.Add(new DataAccess
                    {
                        UserId = UserId,
                        ItemId = newCase.Id,
                        RestrictedType = RestrictedType.LegalAppCase,
                        CreatedBy = UserEmail
                    });
                    await _context.SaveChangesAsync();
                }
                
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut("client/{clientId}/case/{caseId}/update")]
        public async Task<IActionResult> UpdateCase(long clientId, long caseId, [FromBody] CaseForm form)
        {
            try
            {
                var entity = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .FirstOrDefault(); 
                
                if (entity is null) return BadRequest();

                entity.Description = form.Description;
                entity.Name = form.Name;
                entity.Signature = form.Signature;
                entity.Group = form.Group;

                await _context.SaveChangesAsync();
                return Ok();
                
                // var check = await CheckAccess(RestrictedType.LegalAppCase, caseId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entity = _context.LegalAppCases
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClientId == _context.LegalAppClients
                //                 .FirstOrDefault(y =>
                //                     y.Id == clientId &&
                //                     y.LegalAppAccessKeyId == check.LegalAppAccessKey.Id).Id &&
                //             x.Id == caseId)
                //         .FirstOrDefault();
                //
                //
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
        
        [HttpDelete("client/{clientId}/case/{caseId}")]
        public async Task<IActionResult> DeleteCase(long clientId, long caseId)
        {
            try
            {
                var entity = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .FirstOrDefault(); 
                
                if (entity is null) return BadRequest();
                
                _context.Remove(entity);
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
                //             x.Id == caseId)
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
        
        [HttpPut("client/{clientId}/case/{caseId}/archive")]
        public async Task<IActionResult> ArchiveCase(long clientId, long caseId)
        {
            try
            {
                var entity = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context)
                    .FirstOrDefault(); 
                
                if (entity is null) return BadRequest();
                
                entity.Active = !entity.Active;
                entity.UpdatedBy = UserEmail;
                entity.Updated = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return Ok();
                
                // var check = await CheckAccess(RestrictedType.LegalAppCase, caseId);
                // if (check.LegalAppAccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                // if (check.DataAccessAllowed)
                // {
                //     var entity = _context.LegalAppCases
                //         .Include(x => x.LegalAppClient)
                //         .Where(x =>
                //             x.Active &&
                //             x.LegalAppClient.Id == clientId &&
                //             x.LegalAppClient.LegalAppAccessKeyId == check.LegalAppAccessKey.Id)
                //         .FirstOrDefault();
                //     
                //     if (entity is null) return StatusCode(StatusCodes.Status403Forbidden);
                //
                //     entity.Active = !entity.Active;
                //     entity.UpdatedBy = UserEmail;
                //     entity.Updated = DateTime.UtcNow;
                //
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