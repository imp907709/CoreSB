using CoreSB.Universal.Infrastructure.Mongo;
using MongoDB.Driver;

namespace CoreSB.Domain.Currency.Mongo
{
    public interface ICurrencyMongoContext : IMongoContext
    {
        public IMongoCollection<CurrencyMongoDAL> currencies { get; }
        public IMongoCollection<CurrencyRatesMongoDAL> rates{ get; }
    }
}
