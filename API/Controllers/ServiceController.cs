using System.Threading.Tasks;
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

        internal IServiceEF _service;

        public ServiceController(IServiceEF service, IMapper mapper, IValidatorCustom validator, ILoggerCustom logger)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            return $"Service: {_service.GetType()} {_service._status.Message}" ;
        }
        
        [HttpGet]
        [Route("getConnectionString")]
        public ActionResult<string> GetConnectionString()
        {
            return $"Write service connstr: {_service.GetConnectionString()}" ;
        }
        
    }
}
