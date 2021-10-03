using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp.Billing;
using SystemyWP.API.Projections.LegalApp.Billing;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Access;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemyWP.Data.Models.LegalAppModels.Billings;

namespace SystemyWP.API.Controllers.LegalApp.Billing
{
    [Route("/api/legal-app-billing")]
    [Authorize(SystemyWpConstants.Policies.UserAdmin)]
    public class LegalAppBillingController : LegalAppApiController
    {
        public LegalAppBillingController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBillings()
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (admin?.LegalAccessKey is null || admin.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest();

                var result = _context.LegalAppBillingRecords
                    .Where(x =>
                        x.LegalAccessKeyId == admin.LegalAccessKey.Id)
                    .Select(LegalAppBillingProjections.Projection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpGet("get/{billingId}")]
        public async Task<IActionResult> GetBilling(long billingId)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (admin?.LegalAccessKey is null || admin.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest();

                var result = _context.LegalAppBillingRecords
                    .Where(x =>
                        x.LegalAccessKeyId == admin.LegalAccessKey.Id &&
                        x.Id == billingId)
                    .Select(LegalAppBillingProjections.Projection)
                    .FirstOrDefault();
                if (result is null) return BadRequest();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBilling([FromBody] LegalAppBillingDataRecordForm form)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (admin?.LegalAccessKey is null || admin.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest();

                var newBilling = new LegalAppBillingRecord
                {
                    LegalAccessKey = admin.LegalAccessKey,
                    CreatedBy = Username,
                    UpdatedBy = Username,
                    Name = form.Name,
                    Street = form.Street,
                    Address = form.Address,
                    City = form.City,
                    PhoneNumber = form.PhoneNumber,
                    FaxNumber = form.FaxNumber,
                    PostalCode = form.PostalCode,
                    Nip = form.Nip,
                    Regon = form.Regon
                };

                _context.Add(newBilling);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("update/{billingId}")]
        public async Task<IActionResult> UpdateBilling(long billingId, [FromBody] LegalAppBillingDataRecordForm form)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (admin?.LegalAccessKey is null || admin.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest();

                var result = _context.LegalAppBillingRecords
                    .Where(x =>
                        x.LegalAccessKeyId == admin.LegalAccessKey.Id &&
                        x.Id == billingId)
                    .FirstOrDefault();
                if (result is null) return BadRequest();
                
                result.Updated = DateTime.UtcNow;
                result.UpdatedBy = Username;
                result.Name = form.Name;
                result.Street = form.Street;
                result.Address = form.Address;
                result.City = form.City;
                result.PhoneNumber = form.PhoneNumber;
                result.FaxNumber = form.FaxNumber;
                result.PostalCode = form.PostalCode;
                result.Nip = form.Nip;
                result.Regon = form.Regon;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
        
        [HttpDelete("delete/{billingId}")]
        public async Task<IActionResult> DeleteBilling(long billingId)
        {
            try
            {
                var admin = _context.Users
                    .Include(x => x.LegalAccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));
                if (admin?.LegalAccessKey is null || admin.LegalAccessKey?.ExpireDate <= DateTime.UtcNow) return BadRequest();

                var result = _context.LegalAppBillingRecords
                    .Where(x =>
                        x.LegalAccessKeyId == admin.LegalAccessKey.Id &&
                        x.Id == billingId)
                    .FirstOrDefault();
                if (result is null) return BadRequest();

                _context.Remove(result);
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