using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<string> Add<T>(T item) where T : IMongoDAL
        {
            var c = _repository.GetCollection<T>();
            await c.InsertOneAsync(item);
            return item.Id;
        }
    }
}
