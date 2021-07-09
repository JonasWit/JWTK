using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.CustomExtensions.LegalAppExtensions.Reminders;
using SystemyWP.API.Forms.LegalApp.Reminders;
using SystemyWP.API.Projections.LegalApp.Reminders;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp.Reminders
{
    [Route("/api/legal-app-reminders")]
    [Authorize(SystemyWpConstants.Policies.User)]
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
                return ServerError;
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

                return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectParameters);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
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
                return ServerError;
            }
        }
        
        [HttpGet("categories")]
        public async Task<IActionResult> GetReminderCategories()
        {
            try
            {
                var result = new List<object>();
                foreach(ReminderCategory cat in Enum.GetValues(typeof(ReminderCategory)) )
                {
                    result.Add(new {
                        key = (int)cat,
                        value = cat.ToString()
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("reminder")]
        public async Task<IActionResult> CreateReminder([FromBody] ReminderForm form)
        {
            try
            {
                if (form.Start > form.End) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectParameters);
                
                var user = await _context.Users
                    .Include(x => x.LegalAppAccessKey)
                    .FirstOrDefaultAsync(x => x.Id.Equals(UserId));
                if (user?.LegalAppAccessKey is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                var reminder = new LegalAppReminder
                {
                    ReminderCategory = form.ReminderCategory,
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
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPut("reminder/{reminderId}")]
        public async Task<IActionResult> UpdateReminder(long reminderId, [FromBody] ReminderForm form)
        {
            try
            {
                if (form.Start >= form.End) return BadRequest(SystemyWpConstants.ResponseMessages.IncorrectParameters);
                
                var reminder = _context.LegalAppReminders
                    .GetReminder(UserId, Role, reminderId, _context)
                    .FirstOrDefault();

                if (reminder is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                reminder.ReminderCategory = form.ReminderCategory;
                reminder.Active = form.Active;
                reminder.Updated = DateTime.UtcNow;
                reminder.UpdatedBy = UserEmail;
                reminder.Public = form.Public;
                reminder.Name = form.Name;
                reminder.Message = form.Message;
                reminder.Start = form.Start;
                reminder.End = form.End;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
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
                
                if (reminder is null) return BadRequest(SystemyWpConstants.ResponseMessages.NoAccess);

                _context.Remove(reminder);
                await _context.SaveChangesAsync();
                return Ok(reminder);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }
    }
}