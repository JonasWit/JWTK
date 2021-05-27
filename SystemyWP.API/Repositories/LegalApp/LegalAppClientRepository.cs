using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Repositories.Base;
using SystemyWP.API.Repositories.Portal;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Repositories.LegalApp
{
    public class LegalAppClientRepository : LegalAppRepositoryBase
    {
        public LegalAppClientRepository(AppDbContext context, PortalLogger logger) :
            base(context, logger)
        {
        }

        public async Task<RepositoryActionResult<IEnumerable<object>>> GetClientsBasicList(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult<IEnumerable<object>>
                        {StatusCode = StatusCodes.Status403Forbidden};

                var result = new List<object>();

                //Get data as Admin
                if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                    Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                {
                    result.AddRange(_context.LegalAppClients
                        .Where(x =>
                            x.AccessKeyId == check.AccessKey.Id && x.Active)
                        .Select(LegalAppClientProjections.BasicProjection)
                        .ToList());

                    return new RepositoryActionResult<IEnumerable<object>>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Result = result
                    };
                }

                //Get data as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    result.AddRange(_context.LegalAppClients
                        .Where(x =>
                            x.AccessKeyId == check.AccessKey.Id && x.Active &&
                            _context.DataAccesses.Where(y => y.UserId.Equals(UserId))
                                .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                          y.ItemId == x.Id))
                        .Select(LegalAppClientProjections.BasicProjection)
                        .ToList());

                    return new RepositoryActionResult<IEnumerable<object>>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Result = result
                    };
                }

                return new RepositoryActionResult<IEnumerable<object>>
                    {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting basic list", "Exception in Legal App Repository");
                return new RepositoryActionResult<IEnumerable<object>>
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }
        
        public async Task<RepositoryActionResult<object>> GetClient(ClaimsPrincipal claimsPrincipal, long clientId)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null)
                    return new RepositoryActionResult<object> {StatusCode = StatusCodes.Status403Forbidden};

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Where(x => x.AccessKeyId == check.AccessKey.Id && x.Id == clientId)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .FirstOrDefault();

                    return new RepositoryActionResult<object>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Result = result
                    };
                }

                return new RepositoryActionResult<object> {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting specific client", "Exception in Legal App Repository");
                return new RepositoryActionResult<object>
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }

        public async Task<RepositoryActionResult<IEnumerable<object>>> GetClients(ClaimsPrincipal claimsPrincipal, int cursor, int take)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult<IEnumerable<object>> {StatusCode = StatusCodes.Status403Forbidden};
                
                var result = new List<object>();

                //Get data as Admin
                if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                    Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                {
                    result.AddRange(_context.LegalAppClients
                        .Where(x =>
                            x.AccessKeyId == check.AccessKey.Id && x.Active)
                        .OrderBy(x => x.Name)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .ToList());

                    return new RepositoryActionResult<IEnumerable<object>>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Result = result
                    };
                }

                //Get data as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    result.AddRange(_context.LegalAppClients
                        .Where(x =>
                            x.AccessKeyId == check.AccessKey.Id && x.Active && 
                            _context.DataAccesses
                                .Where(y => y.UserId.Equals(UserId))
                                .Any(y => 
                                    y.RestrictedType == RestrictedType.LegalAppClient && y.ItemId == x.Id))
                        .OrderBy(x => x.Name)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppClientProjections.FlatProjection)
                        .ToList());

                    return new RepositoryActionResult<IEnumerable<object>>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Result = result
                    };
                }

                return new RepositoryActionResult<IEnumerable<object>> {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult<IEnumerable<object>>
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }

        public async Task<RepositoryActionResult> CreateClient(ClaimsPrincipal claimsPrincipal, CreateClientForm form)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};

                var newEntity = new LegalAppClient
                {
                    AccessKey = check.AccessKey,
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
                return new RepositoryActionResult {StatusCode = StatusCodes.Status200OK};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }
        
        public async Task<RepositoryActionResult> UpdateClient(ClaimsPrincipal claimsPrincipal, long clientId, UpdateClientForm form)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};

                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKeyId == check.AccessKey.Id);
                    if (entity is null) return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
                    
                    entity.UpdatedBy = UserEmail;
                    entity.Updated = DateTime.UtcNow;
                    entity.Name = form.Name;
                    
                    await _context.SaveChangesAsync();
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status200OK};
                }

                return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }

        public async Task<RepositoryActionResult> ArchiveClient(ClaimsPrincipal claimsPrincipal, long clientId)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};

                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKeyId == check.AccessKey.Id);
                    if (entity is null) return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
                    
                    entity.Active = !entity.Active;
                    entity.UpdatedBy = UserEmail;
                    entity.Updated = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status200OK};
                }

                return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }
        
        public async Task<RepositoryActionResult> DeleteClient(ClaimsPrincipal claimsPrincipal, long clientId)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};

                if (check.DataAccessAllowed)
                {
                    var entity = await _context.LegalAppClients
                        .FirstOrDefaultAsync(x => 
                            x.Id == clientId && x.AccessKeyId == check.AccessKey.Id);
                    if (entity is null) return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
                    
                    _context.Remove(entity);

                    await _context.SaveChangesAsync();
                    return new RepositoryActionResult {StatusCode = StatusCodes.Status200OK};
                }

                return new RepositoryActionResult {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }

        public async Task<RepositoryActionResult<IEnumerable<object>>> GetClientsAndCasesForAccess(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                CurrentClaimsPrincipal = claimsPrincipal;
                var check = await CheckAccess();
                if (check.AccessKey is null)
                    return new RepositoryActionResult<IEnumerable<object>> {StatusCode = StatusCodes.Status403Forbidden};

                if (check.DataAccessAllowed)
                {
                    var user = await _context.Users
                        .Where(x => x.Id.Equals(UserId))
                        .Include(x => x.AccessKey)
                        .FirstOrDefaultAsync();

                    var result = await _context.LegalAppClients
                        .Where(x =>
                            x.AccessKeyId== user.AccessKey.Id)
                        .Include(x =>
                            x.LegalAppCases)
                        .Select(LegalAppClientProjections.MinimalProjection)
                        .ToListAsync();

                    await _context.SaveChangesAsync();
                    return new RepositoryActionResult<IEnumerable<object>> {StatusCode = StatusCodes.Status200OK, Result = result};
                }

                return new RepositoryActionResult<IEnumerable<object>> {StatusCode = StatusCodes.Status403Forbidden};
            }
            catch (Exception e)
            {
                await HandleException(e, "Legal app client - getting clients", "Exception in Legal App Repository");
                return new RepositoryActionResult<IEnumerable<object>>
                    {StatusCode = StatusCodes.Status500InternalServerError};
            }
        }
    }
}