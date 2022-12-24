

using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Domain.Currency
{
    public interface ICurrencyServiceEF : IServiceEF
    {
        Task<ICurrencyBL> AddCurrency(ICurrencyBL currency);

        ICurrencyUpdateBL UpdateCurrency(ICurrencyUpdateBL currency);

        IServiceStatus DeleteCurrency(string currencyIso);
        IServiceStatus DeleteCurrency(ICurrencyUpdateBL currency);


        Task<ICurrencyRateAddAPI> AddCurrencyRateQuery(ICurrencyRateAddAPI query);
        Task<IList<CrossCurrenciesAPI>> ValidateCrossRates(ICrossCurrencyValidateCommand command);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);


        void Initialize();
        void ReInitialize();
        void CleanUp();
        
        
        Task ValidateCrudTest();
    }
}
