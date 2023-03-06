﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.Models;
using CoreSB.Universal;

namespace CoreSB.Domain.NewOrder
{
    public interface INewOrderManager : IDomainManager
    {
        Task<ICurrencyBL> AddCurrency(ICurrencyBL currency);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
