using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoRepository : IRepository
    {
        void SetDatabase(string dbName);
        IMongoDatabase GetDb();

        IMongoCollection<T> GetCollection<T>();

        Task<string> AddAsync<T>(T item)
            where T : IMongoDAL;
    }
}
