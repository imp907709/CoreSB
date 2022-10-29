

using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSB.Universal;

namespace CoreSB.Domain.Currency
{
    public interface ICurrencyServiceEF : IService
    {
        Task<ICurrencyBL> AddCurrency(ICurrencyBL currency);

        ICurrencyUpdateBL UpdateCurrency(ICurrencyUpdateBL currency);

        IServiceStatus DeleteCurrency(string currencyIso);
        IServiceStatus DeleteCurrency(ICurrencyUpdateBL currency);


        Task<ICurrencyRateAddAPI> AddCurrencyRateQuerry(ICurrencyRateAddAPI query);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);

        void ReInitialize();
        void CleanUp();
    }
}
