﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Client
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppClientController : LegalAppApiController
    {
        public LegalAppClientController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            try
            {
                var result = _context.LegalAppClients
                    .GetAllowedClients(UserId, Role, _context, true)
                    .Select(LegalAppClientProjections.FlatProjection)
                    .FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("clients/basic-list")]
        public async Task<IActionResult> GetClientsBasicList()
        {
            try
            {
                var result = _context.LegalAppClients
                    .GetAllowedClients(UserId, Role, _context, true)
                    .Select(LegalAppClientProjections.BasicProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients(int cursor, int take)
        {
            try
            {
                var result = _context.LegalAppClients
                    .GetAllowedClients(UserId, Role, _context, true)
                    .OrderBy(x => x.Name)
                    .Skip(cursor)
                    .Take(take)
                    .Select(LegalAppClientProjections.FlatProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] ClientForm form)
        {
            try
            {
                var user = await _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));

                if (user?.LegalAppAccessKey is null) return BadRequest("Access Key not found");

                var newClient = new LegalAppClient
                {
                    LegalAppAccessKey = user.LegalAppAccessKey,
                    Name = form.Name,
                    CreatedBy = UserEmail,
                    UpdatedBy = UserEmail
                };
                _context.Add(newClient);

                //Act as normal as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    _context.Add(new DataAccess
                    {
                        UserId = UserId,
                        ItemId = newClient.Id,
                        RestrictedType = RestrictedType.LegalAppClient,
                        CreatedBy = UserEmail
                    });
                }

                await _context.SaveChangesAsync();
                return Ok(LegalAppClientProjections.CreateFlat(newClient));
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("update/{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, [FromBody] ClientForm form)
        {
            try
            {
                var client = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();

                if (client is null) return BadRequest("Client not found");

                client.UpdatedBy = UserEmail;
                client.Updated = DateTime.UtcNow;
                client.Name = form.Name;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("archive/{clientId}")]
        [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
        public async Task<IActionResult> ArchiveClient(long clientId)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .Include(x => x.LegalAppCases)
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();

                if (legalAppClient is null) return BadRequest("Client not found");

                _context.RemoveRange(_context.DataAccesses
                    .Where(x => x.RestrictedType == RestrictedType.LegalAppClient && x.ItemId == clientId));
                
                _context.RemoveRange(_context.DataAccesses
                    .Where(dataAccess => 
                        dataAccess.RestrictedType == RestrictedType.LegalAppCase && 
                        _context.LegalAppCases
                            .Where(legalAppCase => legalAppCase.LegalAppClientId == clientId)
                            .Any(x => x.Id == dataAccess.ItemId)));

                legalAppClient.Active = !legalAppClient.Active;
                legalAppClient.UpdatedBy = UserEmail;
                legalAppClient.Updated = DateTime.UtcNow;

                foreach (var legalAppCase in legalAppClient.LegalAppCases)
                {
                    switch (legalAppClient.Active)
                    {
                        case true: 
                            legalAppCase.Active = true;
                            break;
                        case false:      
                            legalAppCase.Active = false;
                            break;
                    }
                }
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete/{clientId}")]
        [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
        public async Task<IActionResult> DeleteClient(long clientId)
        {
            try
            {
                var legalAppClient = _context.LegalAppClients
                    .GetAllowedClient(UserId, Role, clientId, _context)
                    .FirstOrDefault();

                if (legalAppClient is null) return BadRequest("Client not found");

                _context.Remove(legalAppClient);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("admin/flat")]
        [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
        public async Task<IActionResult> GetClientsAndCasesForAccess()
        {
            try
            {
                var user = await _context.Users
                    .Where(x => x.Id.Equals(UserId))
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefaultAsync();

                var result = await _context.LegalAppClients
                    .Include(x =>
                        x.LegalAppCases)
                    .Where(x =>
                        x.LegalAppAccessKeyId == user.LegalAppAccessKey.Id)
                    .Select(LegalAppClientProjections.Projection)
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}