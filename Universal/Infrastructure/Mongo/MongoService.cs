using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.Mongo;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoService : IMongoService
    {
        private IMongoRepository _repository;

        public MongoService(IMongoRepository repo)
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
            var c = _repository.GetCollection<T>();
            await c.InsertOneAsync(item);
            return item?.Id;
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
                IsoName = "testiso",
                IsoCode = 123,
                IsMain = true
            };
            await Add(c);
            var curs = await GetAll<CurrencyMongoDAL>(null);

            var cr = new CurrencyRatesMongoDAL {CurrencyFrom = c, CurrencyTo = c, Rate = 5 / 40.0, Date = DateTime.Now};
            await Add(cr);
            var crates = await GetAll<CurrencyRatesMongoDAL>(null);
        }
    }
}
