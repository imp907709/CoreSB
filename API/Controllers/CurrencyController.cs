using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Domain.Currency;
using CoreSB.Universal;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    public class CurrencyController : ServiceController
    {
        private new ICurrencyServiceEF _service;

        public CurrencyController(ICurrencyServiceEF service, IMapper mapper, IValidatorCustom validator, ILoggerCustom logger) : base(service, mapper, validator,logger)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Reinitialize")]
        public async Task<ActionResult> Reinitialize()
        {
            _service.CleanUp();
            _service.ReInitialize();
            return Ok();
        }

        [HttpGet]
        [Route("Check")]
        public async Task<ActionResult> Check()
        {
            var result = await _service.AddCurrency(new CurrencyBL()
            {
                IsMain = false, IsoCode = 840, IsoName = "USD", Name = "United States dollar"
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("ValidateCrossRates")]
        public async Task<IList<CrossCurrenciesAPI>> ValidateCrossRates(ICrossCurrencyValidateAPI request)
        {
            var command = _mapper.Map<ICrossCurrencyValidateCommand>(request);
            var result = await _service.ValidateCrossRates(command);
            return result;
        }
    }
}
