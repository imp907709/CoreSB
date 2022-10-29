using System;

namespace CoreSB.Domain.Currency
{

    public class CurrencyRateAdd : ICurrencyRateAddAPI
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }

    public class GetCurrencyCommand : IGetCurrencyCommand
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public string ThroughCurrency { get; set; }
        public DateTime Date { get; set; }
    }

    public class CrossCurrenciesAPI : ICrossCurrenciesAPI
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Throught { get; set; }
        public decimal Rate { get; set; }
    }

    public class CurrencyBL : ICurrencyBL
    {
        public string Name { get; set; }
        public string IsoName{ get; set; }
        public int IsoCode { get; set; }
        public bool IsMain { get; set; }
    }

    public class CurrencyUpdateBL : ICurrencyUpdateBL
    {
        public string Name { get; set; }
        public string IsoName{ get; set; }
        public int IsoCode { get; set; }
        public bool IsMain { get; set; }
    }

    public class CurrencyCommand
    {
        public string commandType { get; set; }
        public CurrencyBL payload { get; set; }
    }
}
