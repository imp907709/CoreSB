
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSB.Domain.Currency;
using CoreSB.Universal;

namespace CoreSB.Domain.NewOrder
{
    public interface INewOrderManager : IDomainManager
    {
        Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
