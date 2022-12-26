using System;
using System.Collections.Generic;
using CoreSB.Universal;

namespace CoreSB.Domain.Currency.Mongo
{
    
    public class CurrencyMongoDAL : MongoDAL
    {
        public string Name { get; set; }
        public string IsoName { get; set; }
        public int IsoCode { get; set; }
        public bool IsMain { get; set; }
        
    }

    public class CurrencyRatesMongoDAL : MongoDAL
    {
        public CurrencyMongoDAL CurrencyFrom { get; set; }
        public CurrencyMongoDAL CurrencyTo { get; set; }

        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }


}

