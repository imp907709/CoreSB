using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoContext : IContext<IMongoGuidDAL>
    {
        public void SetWorkingDatabase(string dbName);
        
        public IMongoDatabase GetDatabase();
        
        IMongoCollection<T> GetCollection<T>();

        Task<T> GetById<T>(Guid id) where T : IMongoGuidDAL;
        
        public Task<T> Update<T>(T item)
            where T : class, IMongoGuidDAL;

        public Task<T> Upsert<T>(T item)
            where T : class, IMongoGuidDAL;

        public Task<T> Insert<T>(T Item)
            where T : class, IMongoGuidDAL;
        
        Task<T> AddOneAsync<T>(T item, string name);
        Task<IEnumerable<T>> AddManyAsync<T>(ICollection<T> items, string name);

        Task<long> DeleteByFilterAsync<T>(FilterDefinition<T> deleteFilter)
            where T : IMongoGuidDAL;
        
        public FilterDefinitionBuilder<T> GetFilterBuilder<T>()
        {
            return Builders<T>.Filter;
        }
    }
}
