using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Universal.Infrastructure.Mongo
{
    public interface IMongoService : IService
    {
        void SetDb(string dbName);
        Task<Guid?> Add<T>(T item) where T : IMongoGuidDAL;

        Task<ICollection<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : IMongoGuidDAL;

        Task ValidateAllInOne();
    }
}
