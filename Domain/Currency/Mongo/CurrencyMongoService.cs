using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Universal.Infrastructure.Mongo;
using MongoDB.Driver;

namespace CoreSB.Domain.Currency.Mongo
{
    public class CurrencyMongoService : MongoService, ICurrencyMongoService
    {
        internal new ICurrencyMongoContext _repository;
        
        public CurrencyMongoService(ICurrencyMongoContext repo, IMapper mapper) : base(repo, mapper)
        {
            _repository = repo;
        }

        public async Task InitialGen()
        {
            var crs = _mapper.Map<List<CurrencyMongoDAL>>(InitialPreloadData.initialCurrencies);
            var rts1 = InitialPreloadData.CrossCurrencies_mongo_2019();
            var rts2 = InitialPreloadData.CrossCurrencies_mongo_2022();

            await _repository.currencies.InsertManyAsync(crs);
            await _repository.rates.InsertManyAsync(rts1);
            await _repository.rates.InsertManyAsync(rts2);
        }

        public async Task ValidateCrudTest()
        {
            _repository.SetWorkingDatabase("testdb");
            
            await DropDB();
            await CreateDB();

            var c = new CurrencyMongoDAL {Name = "testName", IsoName = "testiso1", IsoCode = 123, IsMain = true};
            await _repository.currencies.InsertOneAsync(c);
            //await Add(c);


            var c2 = new CurrencyMongoDAL {Name = "testName2", IsoName = "testiso2", IsoCode = 1234, IsMain = true};
            //await Add(c2);
            await _repository.currencies.InsertOneAsync(c2);

            var curs1 = await GetByFilter<CurrencyMongoDAL>(s => s.Id != null);

            var cr = new List<CurrencyRatesMongoDAL>()
            {
                new CurrencyRatesMongoDAL {CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now},
                new CurrencyRatesMongoDAL
                {
                    CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now.AddDays(-1)
                },
                new CurrencyRatesMongoDAL
                {
                    CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now.AddDays(-2)
                }
            };
            await _repository.AddManyAsync(cr);
            var crates = await GetByFilter<CurrencyRatesMongoDAL>(s => s.Id != null);


            var toUpdate = await _repository.GetById<CurrencyRatesMongoDAL>(cr[1].Id);
            toUpdate.Rate = 6 / 20.0;
            await _repository.Update(toUpdate);

            var toUpsert = await _repository.GetById<CurrencyRatesMongoDAL>(cr[2].Id);
            toUpsert.Rate = 4 / 10.0;
            await _repository.Upsert(toUpsert);

            await _repository.DeleteAsync(c2);

            var curs2 = await GetByFilter<CurrencyMongoDAL>(s => s.Id != null);

            await InitialGen();

            var с3 =
                await _repository
                    .currencies
                    .Find(s => s.IsMain == false).ToListAsync();
            var c4 =
                await _repository 
                    .rates
                    .Find(s => s.CurrencyFrom.IsoName == "RUB").ToListAsync();

        }
    }
}
