﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp;
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

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
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
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .FirstOrDefault();
                    
                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("clients/basic-list")]
        public async Task<IActionResult> GetClientsBasicList()
        {
            try
            {
                var user = _context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                if (user?.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                var result = new List<object>();

                //Get data as Admin
                if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                    Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                {
                    result.AddRange(_context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x =>
                            x.AccessKey.Id == user.AccessKey.Id && x.Active)
                        .Select(LegalAppClientProjections.BasicProjection)
                        .ToList());

                    return Ok(result);
                }

                //Get data as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    result.AddRange(_context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x =>
                            x.AccessKey.Id == user.AccessKey.Id && x.Active &&
                            _context.DataAccesses.Where(y => y.UserId.Equals(UserId))
                                .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                          y.ItemId == x.Id))
                        .Select(LegalAppClientProjections.BasicProjection)
                        .ToList());

                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
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
                var user = _context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                if (user?.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                var result = new List<object>();

                //Get data as Admin
                if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                    Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                {
                    result.AddRange(_context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x =>
                            x.AccessKey.Id == user.AccessKey.Id && x.Active)
                        .OrderBy(x => x.Name)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .ToList());

                    return Ok(result);
                }

                //Get data as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    result.AddRange(_context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x =>
                            x.AccessKey.Id == user.AccessKey.Id && x.Active && 
                            _context.DataAccesses
                                .Where(y => y.UserId.Equals(UserId))
                                .Any(y => 
                                    y.RestrictedType == RestrictedType.LegalAppClient && y.ItemId == x.Id))
                        .OrderBy(x => x.Name)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .ToList());

                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }  
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientForm form)
        {
            try
            {
                var user = await _context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));

                if (user?.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                
                var newEntity = new LegalAppClient
                {
                    AccessKey = user.AccessKey,
                    Name = form.Name,
                    CreatedBy = UserEmail,
                    UpdatedBy = UserEmail
                };
                _context.Add(newEntity);

                //Act as normal as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    _context.Add(new DataAccess
                    {
                        UserId = UserId,
                        ItemId = newEntity.Id,
                        RestrictedType = RestrictedType.LegalAppClient,
                        CreatedBy = UserEmail
                    });
                }
                await _context.SaveChangesAsync();
                return Ok(LegalAppClientProjections.CreateFlat(newEntity));
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("update/{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, [FromBody] UpdateClientForm form)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                
                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);
                    if (entity is null) return StatusCode(StatusCodes.Status403Forbidden);
                    
                    entity.UpdatedBy = UserEmail;
                    entity.Updated = DateTime.UtcNow;
                    entity.Name = form.Name;
                    
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("archive/{clientId}")]
        public async Task<IActionResult> ArchiveClient(long clientId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                
                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);
                    if (entity is null) return StatusCode(StatusCodes.Status403Forbidden);
                    
                    entity.Active = !entity.Active;
                    entity.UpdatedBy = UserEmail;
                    entity.Updated = DateTime.UtcNow;
                    
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete/{clientId}")]
        public async Task<IActionResult> DeleteClient(long clientId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
                
                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);
                    if (entity is null) return StatusCode(StatusCodes.Status403Forbidden);
                    
                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
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
                    .Include(x => x.AccessKey)
                    .FirstOrDefaultAsync();

                var result = await _context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Id == user.AccessKey.Id)
                    .Include(x =>
                        x.LegalAppCases)
                    .Select(LegalAppClientProjections.MinimalProjection)
                    .ToListAsync();

                return Ok(result);
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