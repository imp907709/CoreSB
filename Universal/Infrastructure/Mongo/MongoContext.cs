using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        private MongoClient _client;
        
        private IMongoDatabase _database;
        private string dbName;

        private MongoClientSettings _settings;

        public MongoContext(MongoClient client)
        {
            _client = client;
        }

        public MongoContext(MongoClient client, string dbName)
        {
            _client = client;
            SetDatabase(dbName);
        }

        public MongoContext(string connString)
        {
            SetClient(connString);
        }

        public MongoContext(string connString, string dbName)
        {
            SetClient(connString);
            SetDatabase(dbName);
        }

        public void SetDatabase(string dbName)
        {
            _database = _client.GetDatabase(dbName);
            this.dbName = dbName;
        }

        public IMongoDatabase GetDb()
        {
            return this._database;
        }

        void SetClient(string connString)
        {
            _settings = MongoClientSettings.FromConnectionString(connString);
            _settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _client = new MongoClient(_settings);
        }


        
        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
            
        
        public async Task<ICollection<T>> GetAll<T>(Expression<Func<T, bool>> expression) 
            where T : IMongoGuidDAL
        {
            var items = await GetCollection<T>().Find(expression).ToListAsync();
            return items;
        }

        public async Task<Guid?> AddOneAsync<T>(T item)
            where T : IMongoGuidDAL
        {
            var c = GetCollection<T>();
            await c.InsertOneAsync(item);
            return item?.Id;
        }
        
        public async Task<IEnumerable<Guid>> AddManyAsync<T>(ICollection<T> items)
            where T : IMongoGuidDAL
        {
            var c = GetCollection<T>();
            await c.InsertManyAsync(items);
            return items.Select(s=>s.Id);
        }

        public async Task<long> DeleteAsync<T>(T item)
            where T : IMongoGuidDAL
        {
            var deleteFilter = Builders<T>.Filter.Eq(s=> s.Id, item.Id);
            var c = await GetCollection<T>()
                .DeleteOneAsync(deleteFilter);
            return c.DeletedCount;
        }
        public async Task<long> DeleteManyAsync<T>(DateTime created)
            where T : ICreateDateDAL
        {
            var deleteFilter = Builders<T>.Filter.Lte(s => s.Created, created);
            var c = await GetCollection<T>()
                .DeleteOneAsync(deleteFilter);
            return c.DeletedCount;
        }
        public async Task<long> DeleteByFilterAsync<T>(FilterDefinition<T> deleteFilter)
            where T : IMongoGuidDAL
        {
            var c = await GetCollection<T>()
                .DeleteOneAsync(deleteFilter);
            return c.DeletedCount;
        }


        public void Add<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void AddRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void DeleteRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void UpdateRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SkipTake<T>(int skip, int take) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task DropDB()
        {
            await _client.DropDatabaseAsync(dbName);
        }

        public void DropDbSync()
        {
            _client.DropDatabase(dbName);
        }

        public async Task CreateDB()
        {
            _client.GetDatabase(this.dbName);
        }

        public void ReInitialize()
        {
            throw new NotImplementedException();
        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }

        public string GetDatabaseName()
        {
            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
