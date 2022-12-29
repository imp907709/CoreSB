using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.Mongo;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace CoreSB.Domain.Currency.Mongo
{
    public class CurrencyMongoContext : MongoContext, ICurrencyMongoContext
    {
        public IMongoCollection<CurrencyMongoDAL> currencies { get; private set; }
        public IMongoCollection<CurrencyRatesMongoDAL> rates { get; private set; }
        
        static CurrencyMongoContext()
        {
            RegisterClasses();
        }
        public CurrencyMongoContext(MongoClient client) : base(client)
        {
            RegisterCollections();
        }
        public CurrencyMongoContext(string connString, string dbName) : base(connString, dbName)
        {
            RegisterCollections();
        }

        static void RegisterClasses()
        {
            // RegisterAutoMap<CurrencyMongoDAL>();
            // RegisterAutoMap<CurrencyRatesMongoDAL>();

            BsonClassMap.GetRegisteredClassMaps();
            
            BsonClassMap.RegisterClassMap<MongoDAL>(m =>
            {
                m.AutoMap();
                m.MapIdMember(x => x.Id).SetIdGenerator(new GuidGenerator());
                m.SetIsRootClass(true);
                m.SetIgnoreExtraElements(true);
            });

            RegisterAutoMap<CurrencyMongoDAL>();
            RegisterAutoMap<CurrencyRatesMongoDAL>();
        }
        static void RegisterAutoMap<T>()
            where T : MongoDAL
        {
            BsonClassMap.RegisterClassMap<T>(c =>
            {
                c.AutoMap();
            });
        }

        void RegisterCollections()
        {
            currencies = GetCollection<CurrencyMongoDAL>("Currencies");
            rates = GetCollection<CurrencyRatesMongoDAL>("CurrencyRates");
        }
        
        public new void SetWorkingDatabase(string dbName)
        {
            _database = _client.GetDatabase(dbName);
            this.dbName = dbName;
            RegisterCollections();
        }
    }
}
