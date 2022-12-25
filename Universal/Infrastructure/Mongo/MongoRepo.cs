using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private string dbName;

        private MongoClientSettings _settings;

        public MongoRepository(MongoClient client)
        {
            _client = client;
        }

        public MongoRepository(MongoClient client, string dbName)
        {
            _client = client;
            SetDatabase(dbName);
        }

        public MongoRepository(string connString)
        {
            SetClient(connString);
        }

        public MongoRepository(string connString, string dbName)
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
            
        
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddAsync<T>(T item)
            where T : IMongoDAL
        {
            var c = GetCollection<T>();
            await c.InsertOneAsync(item);
            return item?.Id;
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
