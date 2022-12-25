using AutoMapper;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Infrastructure.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ServiceController : ControllerBase
    {
        internal IMapper _mapper;
        internal IValidatorCustom _validator;
        internal ILoggerCustom _logger;

        internal IServiceEF _serviceEF;

        public ServiceController(IServiceEF serviceEF, IMapper mapper,
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
            return $"Write service connstr: {_serviceEF.GetConnectionString()}";
        }
    }
}
