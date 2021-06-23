using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Reminders;
using SystemyWP.API.Forms.LegalApp.Reminders;
using SystemyWP.API.Projections.LegalApp.Reminders;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Reminders
{
    [Route("/api/legal-app-reminders")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppRemindersController : LegalAppApiController
    {
        public LegalAppRemindersController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("list")]
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

        [HttpGet("list/limit")]
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

        [HttpGet("reminder/{reminderId}")]
        public async Task<IActionResult> GetReminder(long reminderId)
        {
            try
            {
                var reminder = _context.LegalAppReminders
                    .GetReminder(UserId, Role, reminderId, _context)
                    .Select(LegalAppRemindersProjections.Projection)
                    .FirstOrDefault();

                return Ok(reminder);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("reminder")]
        public async Task<IActionResult> CreateReminder([FromBody] ReminderForm form)
        {
            try
            {
                if (form.Start >= form.End) return BadRequest();
                
                var user = await _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user?.LegalAppAccessKey is null) return BadRequest();

                var reminder = new LegalAppReminder
                {
                    LegalAppAccessKey = user.LegalAppAccessKey,
                    Name = form.Name,
                    Start = form.Start,
                    End = form.End,
                    Message = form.Message,
                    Public = form.Public,
                    CreatedBy = UserEmail,
                    UpdatedBy = UserEmail,
                    AuthorId = UserId
                };

                _context.Add(reminder);
                await _context.SaveChangesAsync();
                return Ok(reminder);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("reminder/{reminderId}")]
        public async Task<IActionResult> UpdateReminder(long reminderId, [FromBody] ReminderForm form)
        {
            try
            {
                if (form.Start >= form.End) return BadRequest();
                
                var reminder = _context.LegalAppReminders
                    .GetReminder(UserId, Role, reminderId, _context)
                    .FirstOrDefault();

                if (reminder is null) return BadRequest();
                
                reminder.Updated = DateTime.UtcNow;
                reminder.UpdatedBy = UserEmail;
                reminder.Public = form.Public;
                reminder.Name = form.Name;
                reminder.Message = form.Message;
                reminder.Start = form.Start;
                reminder.End = form.End;
                
                await _context.SaveChangesAsync();
                return Ok(reminder);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpDelete("reminder/{reminderId}")]
        public async Task<IActionResult> DeleteReminder(long reminderId)
        {
            try
            {
                var reminder = _context.LegalAppReminders
                    .GetReminder(UserId, Role, reminderId, _context)
                    .FirstOrDefault();
                
                if (reminder is null) return BadRequest();

                _context.Remove(reminder);
                await _context.SaveChangesAsync();
                return Ok(reminder);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}