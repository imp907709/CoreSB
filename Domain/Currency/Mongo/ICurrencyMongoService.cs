using System.Threading.Tasks;
using CoreSB.Universal.Infrastructure.Mongo;

namespace CoreSB.Domain.Currency.Mongo
{
    public interface ICurrencyMongoService : IMongoService
    {
        Task InitialGen();
        Task ValidateCrudTest();
    }
}
