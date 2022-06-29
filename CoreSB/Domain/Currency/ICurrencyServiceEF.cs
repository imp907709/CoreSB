

using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSB.Universal;

namespace CoreSB.Domain.Currency
{
    public interface ICurrencyServiceEF : IService
    {
        Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency);

        ICurrencyUpdateAPI UpdateCurrency(ICurrencyUpdateAPI currency);

        IServiceStatus DeleteCurrency(string currencyIso);
        IServiceStatus DeleteCurrency(ICurrencyUpdateAPI currency);


        Task<ICurrencyRateAddAPI> AddCurrencyRateQuerry(ICurrencyRateAddAPI query);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
