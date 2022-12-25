using System.Threading.Tasks;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoService : IService
    {
        void SetDb(string dbName);
        Task<string> Add<T>(T item) where T : IMongoDAL;
    }
}
