using System.Threading.Tasks;
using CoreSB.Universal;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ServiceController : ControllerBase
    {
        internal IService _service;
        
        public ServiceController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            return _service._status.Message;
        }
    }
}
