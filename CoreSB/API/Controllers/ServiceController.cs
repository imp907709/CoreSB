using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Universal;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ServiceController : ControllerBase
    {
        internal IService _service;
        internal IMapper _mapper;
        internal IValidatorCustom _validator;
        internal ILoggerCustom _logger;

        public ServiceController(IService service, IMapper mapper, IValidatorCustom validator, ILoggerCustom logger)
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
    }
}
