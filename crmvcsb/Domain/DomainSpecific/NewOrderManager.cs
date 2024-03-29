﻿
namespace crmvcsb.Domain.DomainSpecific.NewOrder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.Logging;

    using crmvcsb.Domain.Universal;
    using crmvcsb.Domain.DomainSpecific.Currency;
    using crmvcsb.Domain.DomainSpecific.Currency.API;

    public class NewOrderManager : IDomainManager
    {

        public static IConfigurationRoot configuration { get; set; }

        private static ILogger _logger;

        private static INewOrderService _newOrderService { get; set; }
        private static ICurrencyService _currencyService { get; set; }

        public void BindService(INewOrderService newOrderService, ICurrencyService currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }

        public string GetDbName([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            return _newOrderService.GetDbName();
        }
        public void ReInitialize([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.ReInitialize();
            _currencyService.ReInitialize();
        }
        public void CleanUp([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.CleanUp();
        }


        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
        {
            return await _currencyService.GetCurrencyCrossRatesAsync(command);
        }
    }
}
