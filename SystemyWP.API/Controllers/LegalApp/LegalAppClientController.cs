using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp;
using SystemyWP.API.Projections.LegalApp;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWPConstants.Policies.Client)]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppClientController : ApiController
    {
        public LegalAppClientController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("client/{clientId}")]
        public IActionResult GetClient(
            int clientId)
        {
            var user = _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefault(x => x.Id.Equals(UserId));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            //Get data as Admin
            if (Role.Equals(SystemyWPConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWPConstants.Roles.PortalAdmin))
            {
                var result = _context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x => x.AccessKey.Id == user.AccessKey.Id)
                    .Select(LegalAppClientProjections.FlatProjection)
                    .FirstOrDefault();

                return Ok(result);
            }

            //Get data as User
            if (Role.Equals(SystemyWPConstants.Roles.Client))
            {
                var result = _context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x => x.AccessKey.Id == user.AccessKey.Id &&
                                _context.DataAccesses.Where(y => y.UserId.Equals(UserId))
                                    .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                              y.ItemId == x.Id))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .FirstOrDefault();

                if (result is null) return StatusCode(403);

                return Ok(result);
            }

            return BadRequest("Wystąpił błąd!");
        }

        [HttpGet("clients")]
        public IActionResult GetClientsData()
        {
            var user = _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefault(x => x.Id.Equals(UserId));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            var result = new List<object>();

            //Get data as Admin
            if (Role.Equals(SystemyWPConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWPConstants.Roles.PortalAdmin))
            {
                result.AddRange(_context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .ToList());

                return Ok(result);
            }

            //Get data as User
            if (Role.Equals(SystemyWPConstants.Roles.Client))
            {
                //var allowedData = context.DataAccesses.Where(x => x.UserId.Equals(UserId));

                result.AddRange(_context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name) &&
                        _context.DataAccesses.Where(x => x.UserId.Equals(UserId))
                            .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                      y.ItemId == x.Id))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .ToList());

                return Ok(result);
            }

            return BadRequest("Brak dostępu!");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientForm form)
        {
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");
            _context.Add(new LegalAppClient
            {
                AccessKey = user.AccessKey,
                Name = form.Name,
                CreatedBy = UserId,
                UpdatedBy = UserId
            });

            return Ok();
        }

        [HttpPut("update/{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, [FromBody] CreateClientForm form)
        {
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            var entity = await _context.LegalAppClients
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id == clientId && x.AccessKey.Id == user.AccessKey.Id);
            if (entity is null) return BadRequest("Klient nie istnieje!");

            entity.UpdatedBy = UserId;
            entity.Updated = DateTime.UtcNow;
            entity.Name = form.Name;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("archive/{clientId}")]
        public async Task<IActionResult> ArchiveClient(long clientId)
        {
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            var entity = await _context.LegalAppClients
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id == clientId && x.AccessKey.Id == user.AccessKey.Id);
            if (entity is null) return BadRequest("Klient nie istnieje!");

            entity.Active = !entity.Active;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("delete/{clientId}")]
        public async Task<IActionResult> DeleteClient(long clientId)
        {
            var user = await _context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            var entity = await _context.LegalAppClients
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id == clientId && x.AccessKey.Id == user.AccessKey.Id);
            if (entity is null) return BadRequest("Klient nie istnieje!");

            entity.Active = !entity.Active;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("admin/flat")]
        [Authorize(SystemyWPConstants.Policies.ClientAdmin)]
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
                return BadRequest();
            }
        }
    }
}