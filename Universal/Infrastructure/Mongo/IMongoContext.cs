using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoContext
    {
        void SetDatabase(string dbName);
        IMongoDatabase GetDb();

        IMongoCollection<T> GetCollection<T>();

        Task<ICollection<T>> GetAll<T>(Expression<Func<T, bool>> expression)
            where T : IMongoGuidDAL;

        void DropDbSync();


        public Task<Guid?> AddOneAsync<T>(T item)
            where T : IMongoGuidDAL;

        public Task<IEnumerable<Guid>> AddManyAsync<T>(ICollection<T> items)
            where T : IMongoGuidDAL;


        Task<long> DeleteAsync<T>(T item)
            where T : IMongoGuidDAL;

        Task<long> DeleteManyAsync<T>(DateTime created)
            where T : ICreateDateDAL;

        Task<long> DeleteByFilterAsync<T>(FilterDefinition<T> deleteFilter)
            where T : IMongoGuidDAL;
    }
}
