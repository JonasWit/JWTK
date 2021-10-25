﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly PortalLogger _portalLogger;
        protected readonly AppDbContext _context;

        protected IActionResult ServerError => StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd, skontaktuj się z Administratorem");

        public ApiController(PortalLogger portalLogger, AppDbContext context)
        {
            _portalLogger = portalLogger;
            _context = context;
        }

        protected string UserId => GetClaim(ClaimTypes.NameIdentifier);
        protected string Username => GetClaim(ClaimTypes.Name);
        protected string UserEmail => GetClaim(ClaimTypes.Email);
        protected string Role => GetClaim(SystemyWpConstants.Claims.Role);

        protected Task HandleException(Exception ex)
        {
            return _portalLogger.Log(new PortalLogRecord
            {
                Created = DateTime.UtcNow,
                CreatedBy = UserEmail,
                LogType = LogType.Exception,
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                Endpoint = HttpContext.Request.Path.Value,
                UserEmail = UserEmail,
                UserId = UserId,
                UserName = Username,
                Description = "Exception in Controller"
            });
        }
 
        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        protected bool LegalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWpConstants.Apps.LegalApp));
        
        protected bool MedicalAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWpConstants.Apps.MedicalApp));
        
        protected bool RestaurantAppAllowed => User.Claims
            .Any(x => x.Type.Equals(SystemyWpConstants.Claims.AppAccess) && 
                      x.Value.Equals(SystemyWpConstants.Apps.RestaurantApp));
    }
}