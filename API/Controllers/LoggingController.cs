using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreSB.API.Models;
using CoreSB.Domain.Logging;
using CoreSB.Domain.Logging.Models;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Infrastructure.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoggingController : ControllerBase
    {
        internal IMapper _mapper;
        internal IValidatorCustom _validator;
        internal ILoggerCustom _logger;

        internal ILoggingServiceEF _serviceEF;

        public LoggingController(ILoggingServiceEF serviceEF, IMapper mapper, 
            IValidatorCustom validator, ILoggerCustom logger)
        {
            _serviceEF = serviceEF;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            return $"Service: {_serviceEF.GetType()} {_serviceEF._status.Message}";
        }

        [HttpGet]
        [Route("getConnectionString")]
        public ActionResult<string> GetConnectionString()
        {
            #if DEBUG
                return $"Write service connstr: {_serviceEF.GetConnectionString()}";
            #endif
        }

        [HttpGet]
        [Route("getLogs")]
        public ActionResult<IList<LoggingDAL>> GetLogging(PagingAPI PagingAPI)
        {
            return _serviceEF.GetPaging<LoggingDAL>(PagingAPI.Page, PagingAPI.PerPage)
                .ToList();
        }
        
    }
}
