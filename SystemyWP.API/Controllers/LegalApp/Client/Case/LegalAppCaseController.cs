using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Cases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.LegalApp.Client.Case;
using SystemyWP.API.Projections.LegalApp.Cases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client.Case
{
    [Route("/api/legal-app-cases")]
    [Authorize(SystemyWpConstants.Policies.User)]
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
                return ServerError;
            }
        }
        
        [HttpGet("client/{clientId}/cases/archive")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> GetArchivedCases(long clientId)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCases(UserId, Role, clientId, _context, false)
                    .Select(LegalAppCaseProjections.LinkedProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("case/{caseId}")]
        public async Task<IActionResult> GetCase(long caseId)
        {
            try
            {
                var result = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .Select(LegalAppCaseProjections.Projection)
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

        [HttpPost("client/{clientId}/case")]
        public async Task<IActionResult> CreateCase(long clientId, [FromBody] CaseForm form)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context, true)
                    .FirstOrDefault();
                if (legalAppClient is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var user = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id == UserId);
                if (user is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                var newCase = new LegalAppCase()
                {
                    Group = form.Group,
                    Description = form.Description,
                    Signature = form.Signature,
                    Name = form.Name,
                    CreatedBy = Username,
                    UpdatedBy = Username
                };
                
                legalAppClient.LegalAppCases.Add(newCase);
                await _context.SaveChangesAsync();
                
                //Act as normal as User
                if (Role.Equals(SystemyWpConstants.Roles.User))
                {
                    _context.Add(new LegalAppDataAccess
                    {
                        LegalAccessKeyId = user.LegalAccessKey.Id,
                        UserId = UserId,
                        ItemId = newCase.Id,
                        LegalAppRestrictedType = LegalAppRestrictedType.LegalAppCase,
                        CreatedBy = Username
                    });
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpPut("case/{caseId}")]
        public async Task<IActionResult> UpdateCase(long caseId, [FromBody] CaseForm form)
        {
            try
            {
                var legalAppCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .FirstOrDefault(); 
                
                if (legalAppCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                legalAppCase.Description = form.Description;
                legalAppCase.Name = form.Name;
                legalAppCase.Signature = form.Signature;
                legalAppCase.Group = form.Group;
                legalAppCase.UpdatedBy = Username;
                legalAppCase.Updated = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpDelete("case/{caseId}")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> DeleteCase(long caseId)
        {
            try
            {
                var legalAppCase = _context.LegalAppCases
                    .GetAllowedCase(UserId, Role, caseId, _context)
                    .FirstOrDefault(); 
                
                if (legalAppCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.RemoveRange(_context.LegalAppDataAccesses
                    .Where(x =>
                        x.ItemId == legalAppCase.Id &&
                        x.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase));
                
                _context.Remove(legalAppCase);
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpPut("archive/case/{caseId}")]
        [Authorize(SystemyWpConstants.Policies.UserAdmin)]
        public async Task<IActionResult> ArchiveCase(long caseId)
        {
            try
            {
                var legalAppCase = _context.LegalAppCases
                    .GetAllAllowedCases(UserId, Role, caseId, _context)
                    .FirstOrDefault(); 
                
                if (legalAppCase is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);
                
                legalAppCase.Active = !legalAppCase.Active;
                legalAppCase.UpdatedBy = UserEmail;
                legalAppCase.Updated = DateTime.UtcNow;
                
                _context.RemoveRange(_context.LegalAppDataAccesses
                        .Where(x => x.LegalAppRestrictedType == LegalAppRestrictedType.LegalAppCase && x.ItemId == caseId));
                
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