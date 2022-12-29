using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreSB.Universal
{
    public interface IContext<T>
    {
        public Task DropDatabase();

        public void CreateDatabase();
        

        Task<ICollection<T>> GetByFilter<T>(Expression<Func<T, bool>> expression);

        public Task<T> AddOneAsync<T>(T item);

        public Task<IEnumerable<T>> AddManyAsync<T>(ICollection<T> items);
        
        Task<long> DeleteAsync<T>(T item)
            where T : IMongoGuidDAL;

        Task<long> DeleteManyAsync<T>(DateTime created)
            where T : ICreateDateDAL;
    }
}
