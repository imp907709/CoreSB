using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public class MongoService : IMongoService
    {
        private IMongoRepository _repository;

        public MongoService(IMongoRepository repo)
        {
            _repository = repo;
        }

        public Expression<Func<IDateEntityDAL, bool>> CompareByDateExp(DateTime date, ExpressionType direction, Service.DateComparisonRange compareBy)
        {
            throw new NotImplementedException();
        }

        public IRepository GetRepositoryRead()
        {
            throw new NotImplementedException();
        }

        public IRepository GetRepositoryWrite()
        {
            throw new NotImplementedException();
        }

        public Task DropDB()
        {
            throw new NotImplementedException();
        }

        public async Task CreateDB()
        {
            await _repository.CreateDB();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public string actualStatus { get; }
        public IServiceStatus _status { get; }
    }
}
