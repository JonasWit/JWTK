using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Clients;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Reminders;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Projections.LegalApp.Reminders;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Reminders
{
    // todo: finish controller
    [Route("/api/legal-app-reminders")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class RemindersController : LegalAppApiController
    {
        public RemindersController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("/list")]
        public async Task<IActionResult> GetReminders()
        {
            try
            {
                var reminders = _context.LegalAppReminders
                    .GetAllReminders(UserId, Role, _context)
                    .Select(LegalAppRemindersProjections.Projection)
                    .ToList();        
                
                return Ok(reminders);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("/list/limit")]
        public async Task<IActionResult> GetReminders(string from, string to)
        {
            try
            {
                if (DateTime.TryParse(from, out var fromDate) &&
                    DateTime.TryParse(to, out var toDate))
                {
                    var reminders = _context.LegalAppReminders
                        .GetReminders(UserId, Role, fromDate, toDate, _context)
                        .Select(LegalAppRemindersProjections.Projection)
                        .ToList(); 
                    
                    return Ok(reminders);  
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        
        
        
        
        
        
        
    }
}