
using System;
using System.Collections.Generic;
using CoreSB.Universal;

namespace CoreSB.Domain.Currency
{
    public class CurrencyDAL : EntityIntIdDAL
    {
        public string Name { get; set; }
        public string IsoName { get; set; }
        public int IsoCode { get; set; }
        public bool IsMain { get; set; }

        public List<CurrencyRatesDAL> CurRatesFrom { get; set; }
        public List<CurrencyRatesDAL> CurRatesTo { get; set; }
    }

    public class CurrencyRatesDAL : EntityIntIdDAL, IEntityIntIdDAL, IDateEntityDAL
    {
        public CurrencyDAL CurrencyFrom { get; set; }
        public CurrencyDAL CurrencyTo { get; set; }

        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }

        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }


}

