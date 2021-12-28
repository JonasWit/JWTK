using System;
using System.Threading.Tasks;
using AutoMapper;
using SystemyWP.API.Services.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Data.Models.General.Logging;

namespace SystemyWP.API.Controllers.BaseClases
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly PortalLogger _portalLogger;
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        protected IActionResult ServerError => StatusCode(StatusCodes.Status500InternalServerError, "Error occured contact admin");

        protected ApiControllerBase(PortalLogger portalLogger, AppDbContext context, IMapper mapper)
        {
            _portalLogger = portalLogger;
            _context = context;
            _mapper = mapper;
        }

        protected Task NewLog(LogType logType, Exception ex = null, string description = "")
        {
            return _portalLogger.Log(new PortalLogRecord
            {
                LogType = logType,
                ExceptionMessage = ex?.Message,
                ExceptionStackTrace = ex?.StackTrace,
                Endpoint = HttpContext.Request.Path.Value,
                Description = description
            });
        }
    }
}