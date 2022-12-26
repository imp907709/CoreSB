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
        Task<T> Add<T>(T item) where T : IMongoGuidDAL;

        Task ValidateAllInOne();
    }
}
