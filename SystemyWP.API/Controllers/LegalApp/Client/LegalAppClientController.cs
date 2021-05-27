using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Repositories.LegalApp;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
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
        public async Task<IActionResult> GetClient(int clientId, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.GetClient(User, clientId);
            
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok(result.Result);
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("clients/basic-list")]
        public async Task<IActionResult> GetClientsBasicList([FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.GetClientsBasicList(User);
            
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok(result.Result);
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients(int cursor, int take, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.GetClients(User, cursor, take);
            
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok(result.Result);
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            // try
            // {
            //     var user = _context.Users
            //         .Include(x => x.AccessKey)
            //         .FirstOrDefault(x => x.Id.Equals(UserId));
            //
            //     if (user?.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);
            //     var result = new List<object>();
            //
            //     //Get data as Admin
            //     if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
            //         Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
            //     {
            //         result.AddRange(_context.LegalAppClients
            //             .Where(x =>
            //                 x.AccessKeyId == user.AccessKey.Id && x.Active)
            //             .OrderBy(x => x.Name)
            //             .Skip(cursor)
            //             .Take(take)
            //             .Select(LegalAppClientProjections.FlatProjection)
            //             .ToList());
            //
            //         return Ok(result);
            //     }
            //
            //     //Get data as User
            //     if (Role.Equals(SystemyWpConstants.Roles.Client))
            //     {
            //         result.AddRange(_context.LegalAppClients
            //             .Where(x =>
            //                 x.AccessKeyId == user.AccessKey.Id && x.Active && 
            //                 _context.DataAccesses
            //                     .Where(y => y.UserId.Equals(UserId))
            //                     .Any(y => 
            //                         y.RestrictedType == RestrictedType.LegalAppClient && y.ItemId == x.Id))
            //             .OrderBy(x => x.Name)
            //             .Skip(cursor)
            //             .Take(take)
            //             .Select(LegalAppClientProjections.FlatProjection)
            //             .ToList());
            //
            //         return Ok(result);
            //     }
            //
            //     return StatusCode(StatusCodes.Status403Forbidden);
            // }
            // catch (Exception e)
            // {
            //     await HandleException(e);
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }  
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientForm form, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.CreateClient(User, form);
            
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("update/{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, [FromBody] UpdateClientForm form, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.UpdateClient(User, clientId, form);
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("archive/{clientId}")]
        public async Task<IActionResult> ArchiveClient(long clientId, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.ArchiveClient(User, clientId);
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete/{clientId}")]
        public async Task<IActionResult> DeleteClient(long clientId, [FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.ArchiveClient(User, clientId);
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("admin/flat")]
        [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
        public async Task<IActionResult> GetClientsAndCasesForAccess([FromServices] LegalAppClientRepository legalAppClientRepository)
        {
            var result = await legalAppClientRepository.GetClientsAndCasesForAccess(User);
            switch (result.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok();
                case StatusCodes.Status403Forbidden:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                
            }
        }
    }
}