using System;
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

        [HttpGet]
        [Route("dropdb")]
        public async Task<IActionResult> DropDB()
        {
            try
            {
                await _service.DropDB();
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------ERROR>>>");
                Console.WriteLine(e);
                Console.WriteLine("-----------<<<");
                throw;
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("createdb")]
        public async Task<IActionResult> CreateDB()
        {
            try
            {
                await _service.CreateDB();
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------ERROR>>>");
                Console.WriteLine(e);
                Console.WriteLine("-----------<<<");
                throw;
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("initialize")]
        public async Task<IActionResult> Initialize()
        {
            try
            {
                _service.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------ERROR>>>");
                Console.WriteLine(e);
                Console.WriteLine("-----------<<<");
                throw;
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("validateCrudTest")]
        public async Task<IActionResult> ValidateCrudTest()
        {
            try
            {
                await _service.ValidateCrudTest();
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------ERROR>>>");
                Console.WriteLine(e);
                Console.WriteLine("-----------<<<");
                throw;
            }
            return Ok();
        }
    }
}
