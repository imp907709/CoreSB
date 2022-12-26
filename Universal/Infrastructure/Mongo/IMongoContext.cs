using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoContext : IContext<IMongoGuidDAL>
    {

        public IMongoDatabase GetDatabase();
        
        IMongoCollection<T> GetCollection<T>();

        Task<long> DeleteByFilterAsync<T>(FilterDefinition<T> deleteFilter)
            where T : IMongoGuidDAL;
        
        public FilterDefinitionBuilder<T> GetFilterBuilder<T>()
        {
            return Builders<T>.Filter;
        }
    }
}
