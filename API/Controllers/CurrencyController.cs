using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.Mongo;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace CoreSB.API.Controllers
{
    public class CurrencyController : ServiceController
    {
        private new ICurrencyServiceEF _service;
        private new IMongoService _mongoService;
        
        private new IMongoContext _mongoRepository;
        private new ICurrencyMongoService _currencyMongoService;
        
        public CurrencyController(
            ICurrencyServiceEF service, 
            ICurrencyMongoService mongoCurService,
            IMongoService mongoService, 
            IMongoContext mongoRepository,
            IMapper mapper, IValidatorCustom validator, ILoggerCustom logger) 
            : base(service, mapper, validator,logger)
        {
            _service = service;
            _mongoService = mongoService;

            _mongoRepository = mongoRepository;
            _currencyMongoService = mongoCurService;
        }

        [HttpGet]
        [Route("ReinitializeSQL")]
        public async Task<ActionResult> ReinitializeSQL()
        {
            _service.CleanUp();
            _service.ReInitialize();
            return Ok();
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
        [Route("dropdbSQL")]
        public async Task<IActionResult> DropDBSQL()
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
        [Route("createdbSQL")]
        public async Task<IActionResult> CreateDBSQL()
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
        [Route("initializeSQL")]
        public async Task<IActionResult> InitializeSQL()
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

        [HttpGet]
        [Route("validateMongo")]
        public async Task<IActionResult> ValidateMongo()
        {
            await _currencyMongoService.ValidateCrudTest();

            return Ok( );
        }
    }
}
