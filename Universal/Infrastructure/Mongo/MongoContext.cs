using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        internal MongoClient _client;

        internal IMongoDatabase _database;
        internal string dbName;

        internal MongoClientSettings _settings;

        public MongoContext(MongoClient client)
        {
            _client = client;
        }

        public MongoContext(MongoClient client, string dbName)
        {
            _client = client;
            SetWorkingDatabase(dbName);
        }

        public MongoContext(string connString)
        {
            SetClient(connString);
        }
        
        public MongoContext(string connString, string dbName)
        {
            SetClient(connString);
            SetWorkingDatabase(dbName);
        }

        public void SetWorkingDatabase(string dbName)
        {
            _database = _client.GetDatabase(dbName);
            this.dbName = dbName;
        }

        public async Task DropDatabase()
        {
            await _client.DropDatabaseAsync(dbName);
        }

        public void CreateDatabase()
        {
            _client.GetDatabase(this.dbName);
        }

        public IMongoDatabase GetDatabase()
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

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }


        public async Task<T> GetById<T>(Guid id)
            where T : IMongoGuidDAL
        {
            return await GetCollection<T>().Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<T>> GetByFilter<T>(Expression<Func<T, bool>> expression)
        {
            var items = await GetCollection<T>().Find(expression).ToListAsync();
            return items;
        }

        public async Task<T> AddOneAsync<T>(T item)
        {
            var c = GetCollection<T>();
            await c.InsertOneAsync(item);
            return item;
        }

        public async Task<T> AddOneAsync<T>(T item, string name)
        {
            var c = GetCollection<T>(name);
            await c.InsertOneAsync(item);
            return item;
        }

        public async Task<IEnumerable<T>> AddManyAsync<T>(ICollection<T> items)
        {
            var c = GetCollection<T>();
            await c.InsertManyAsync(items);
            return items;
        }

        public async Task<IEnumerable<T>> AddManyAsync<T>(ICollection<T> items, string name)
        {
            var c = GetCollection<T>(name);
            await c.InsertManyAsync(items);
            return items;
        }

        public async Task<T> Update<T>(T item)
            where T : class, IMongoGuidDAL
        {
            var result = await GetCollection<T>().ReplaceOneAsync(s => s.Id == item.Id, item);
            if (result.ModifiedCount > 0)
            {
                return item;
            }

            return null;
        }

        public async Task<T> Insert<T>(T Item)
            where T : class, IMongoGuidDAL
        {
            await GetCollection<T>().InsertOneAsync(Item);
            return Item;
        }

        public async Task<T> Upsert<T>(T item)
            where T : class, IMongoGuidDAL
        {
            var itemToUpdate = await GetById<T>(item.Id);
            if (itemToUpdate != null)
            {
                return await Update(item);
            }

            return await Insert(item);
        }

        public async Task<long> DeleteAsync<T>(T item)
            where T : IMongoGuidDAL
        {
            var deleteFilter = Builders<T>.Filter.Eq(s => s.Id, item.Id);
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

        public FilterDefinitionBuilder<T> GetFilterBuilder<T>()
        {
            return Builders<T>.Filter;
        }
    }
}
