using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Domain.Currency.Mongo;
using MongoDB.Driver;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoService : IMongoService
    {
        internal IMongoContext _context;
        internal IMapper _mapper { get; set; }

        public MongoService(ICurrencyMongoContext repo, IMapper mapper)
        {
            _context = repo;
            _mapper = mapper;
        }

        public void SetDb(string dbName)
        {
            _context.SetWorkingDatabase(dbName);
        }
        public async Task DropDB()
        {
            await _context.DropDatabase();
        }

        public async Task CreateDB()
        {
            await Task.Run(() =>
            {
                _context.CreateDatabase();
            });
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public string actualStatus { get; }
        public IServiceStatus _status { get; }

        public async Task<T> Add<T>(T item) where T : IMongoGuidDAL
        {
            return await _context.AddOneAsync(item);
        }

        public async Task<long> DeleteOlder(DateTime date)
        {
            var filter = Builders<ICreateDateDAL>.Filter.Gte(s => s.Created, date);
            return await _context.DeleteByFilterAsync(filter);
        }
        
        public async Task<ICollection<T>> GetByFilter<T>(Expression<Func<T, bool>> expression)
            where T : IMongoGuidDAL
        {
            return await _context.GetByFilter<T>(expression);
        }

    }
}
