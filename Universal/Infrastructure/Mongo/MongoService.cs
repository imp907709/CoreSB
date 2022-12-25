﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.Mongo;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoService : IMongoService
    {
        private IMongoContext _repository;

        public MongoService(IMongoContext repo)
        {
            _repository = repo;
        }

        public IRepository GetRepositoryRead()
        {
            throw new NotImplementedException();
        }

        public IRepository GetRepositoryWrite()
        {
            return _repository;
        }

        public void SetDb(string dbName)
        {
            _repository.SetDatabase(dbName);
        }
        public async Task DropDB()
        {
            await _repository.DropDB();
        }

        public async Task CreateDB()
        {
            await _repository.CreateDB();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public string actualStatus { get; }
        public IServiceStatus _status { get; }

        public async Task<Guid?> Add<T>(T item) where T : IMongoGuidDAL
        {
            return await _repository.AddOneAsync(item);
        }

        public async Task<long> DeleteOlder(DateTime date)
        {
            var filter = Builders<ICreateDateDAL>.Filter.Gte(s => s.Created, date);
            return await _repository.DeleteByFilterAsync(filter);
        }
        
        public async Task<ICollection<T>> GetAll<T>(Expression<Func<T, bool>> expression)
            where T : IMongoGuidDAL
        {
            return await _repository.GetAll<T>(s=>s.Id != null);
        }

        public async Task ValidateAllInOne()
        {
            SetDb("testdb");
            await DropDB();
            await CreateDB();

            var c = new CurrencyMongoDAL
            {
                Name = "testName",
                IsoName = "testiso1",
                IsoCode = 123,
                IsMain = true
            };
            await Add(c);
          


            var c2 = new CurrencyMongoDAL
            {
                Name = "testName2",
                IsoName = "testiso2",
                IsoCode = 1234,
                IsMain = true
            };
            await Add(c2);
            
            var curs1 = await GetAll<CurrencyMongoDAL>(null);

            var cr = new List<CurrencyRatesMongoDAL>()
            {
                new CurrencyRatesMongoDAL {CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now},
                new CurrencyRatesMongoDAL {CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now.AddDays(-1)},
                new CurrencyRatesMongoDAL {CurrencyFrom = c, CurrencyTo = c2, Rate = 5 / 40.0, Date = DateTime.Now.AddDays(-2)}
            };
            await _repository.AddManyAsync(cr);
            var crates = await GetAll<CurrencyRatesMongoDAL>(null);
            
            await _repository.DeleteAsync(c2);
            
            var curs2 = await GetAll<CurrencyMongoDAL>(null);
        }
    }
}
