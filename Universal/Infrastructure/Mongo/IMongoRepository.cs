using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoRepository : IRepository
    {
        void SetDatabase(string dbName);
        IMongoDatabase GetDb();

        IMongoCollection<T> GetCollection<T>();

        Task<Guid?> AddAsync<T>(T item)
            where T : IMongoGuidDAL;

        Task<ICollection<T>> GetAll<T>(Expression<Func<T, bool>> expression)
            where T : IMongoGuidDAL;

        void DropDbSync();
    }
}
