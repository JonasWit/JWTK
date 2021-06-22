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

namespace SystemyWP.API.Controllers.LegalApp.Client.Case
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
                    .GetAllowedCases(UserId, Role, clientId, _context, true)
                    .Select(LegalAppCaseProjections.BasicProjection)
                    .ToList();

                return Ok(result);
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
                    .GetAllowedCase(UserId, Role, clientId, caseId, _context, true)
                    .Select(LegalAppCaseProjections.Projection)
                    .AsSingleQuery()
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

        [HttpPost("client/{clientId}/case/create")]
        public async Task<IActionResult> CreateCase(long clientId, [FromBody] CaseForm form)
        {
            try
            {
                var appClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context, true)
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
                
                _context.RemoveRange(
                    _context.DataAccesses
                        .Where(x =>
                            x.RestrictedType == RestrictedType.LegalAppCase && x.ItemId == clientId));
                
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