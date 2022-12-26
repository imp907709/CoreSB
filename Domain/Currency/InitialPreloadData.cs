using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreSB.Domain.Currency.Mapping;
using CoreSB.Domain.Currency.Mongo;

namespace CoreSB.Domain.Currency
{
    public class InitialPreloadData
    {
        public static IMapper _mapper { get; set; }

        static InitialPreloadData()
        {
            var cfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrenciesMapping>();
            });
            _mapper = new Mapper(cfg);
        }
        public static List<CurrencyDAL> initialCurrencies => new List<CurrencyDAL>()
        {
            new CurrencyDAL() {Id = 1, Name = "United States dollar", IsoName = "USD", IsoCode = 840, IsMain = false},
            new CurrencyDAL() {Id = 2, Name = "Euro", IsoName = "EUR", IsoCode = 978, IsMain = false},
            new CurrencyDAL() {Id = 3, Name = "Pound sterling", IsoName = "GBP", IsoCode = 826, IsMain = false},
            new CurrencyDAL() {Id = 4, Name = "Russian ruble", IsoName = "RUB", IsoCode = 643, IsMain = false},
                
            new CurrencyDAL() {Id = 5, Name = "Armenian dram", IsoName = "AMD", IsoCode = 051, IsMain = false},
            new CurrencyDAL() {Id = 6, Name = "Georgian lari", IsoName = "GEL", IsoCode = 981, IsMain = false},
            new CurrencyDAL() {Id = 7, Name = "Kazakhstani tenge", IsoName = "KZT", IsoCode = 398, IsMain = false},
                
            new CurrencyDAL() {Id = 11, Name = "JPY", IsoName = "JPY", IsMain = false},
            new CurrencyDAL() {Id = 12, Name = "AUD", IsoName = "AUD", IsMain = false},
            new CurrencyDAL() {Id = 13, Name = "CAD", IsoName = "CAD", IsMain = false},
            new CurrencyDAL() {Id = 14, Name = "CHF", IsoName = "CHF", IsMain = false}
        };

        public static List<CurrencyRatesDAL> CrossCurrencies_2019 => new List<CurrencyRatesDAL>()
        {
            // USD, EUR, GBP / RUB
            new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.16M, Date = new DateTime(2019, 07, 23) },
            new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.42M, Date = new DateTime(2019, 07, 23) },
            new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.57M, Date = new DateTime(2019, 07, 23) },
        };

        public static List<CurrencyRatesDAL> CrossCurrencies_2022 => new List<CurrencyRatesDAL>()
        {
            // USD RUB
            new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 1, CurrencyToId = 4, Rate = 61.53M, Date = new DateTime(2022, 10, 30) },
            // RUB AMD
            new CurrencyRatesDAL() { Id = 11, CurrencyFromId = 4, CurrencyToId = 5, Rate = 6.43M, Date = new DateTime(2022, 10, 30) },
            // USD AMD
            new CurrencyRatesDAL() { Id = 12, CurrencyFromId = 1, CurrencyToId = 5, Rate = 395.53M, Date = new DateTime(2022, 10, 30) }
        };

        public static List<CurrencyRatesMongoDAL> CrossCurrencies_mongo_2019()
        {
            return Remap(CrossCurrencies_2019);
        }
        
        public static List<CurrencyRatesMongoDAL> CrossCurrencies_mongo_2022()
        {
            return Remap(CrossCurrencies_2022);
        }
        
        static List<CurrencyRatesMongoDAL> Remap(List<CurrencyRatesDAL> rates)
        {
            var result = new List<CurrencyRatesMongoDAL>();
            foreach (var i in rates)
            {
                
                var currSQL = _mapper.Map<CurrencyRatesMongoDAL>(i);
                currSQL.CurrencyFrom =
                    _mapper.Map<CurrencyMongoDAL>(
                        InitialPreloadData.initialCurrencies.FirstOrDefault(s => s.Id == i.CurrencyFromId));
                currSQL.CurrencyTo =
                    _mapper.Map<CurrencyMongoDAL>(
                        InitialPreloadData.initialCurrencies.FirstOrDefault(s => s.Id == i.CurrencyToId));
                result.Add(currSQL);
            }

            return result;
        }
    }
}
