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
            await _service.ReInitialize();
            return Ok();
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
        [Route("InitializeSQL")]
        public async Task<IActionResult> ValidateSQL()
        {
            try
            {
                await _service.Initialize();
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
        [Route("InitializeMongo")]
        public async Task<IActionResult> ValidateMongo()
        {
            await _currencyMongoService.InitialGen();

            return Ok( );
        }
    }
}
