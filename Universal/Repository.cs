

/// <summary>
/// Repository domain interface
/// used by interface repositories for DB specific type operations
/// </summary>
namespace CoreSB.Universal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void AddRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void DeleteRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

        public void UpdateRange<T>(IList<T> items) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SkipTake<T>(int skip, int take) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task DropDB()
        {
            throw new NotImplementedException();
        }
        
        public async Task CreateDB()
        {
            throw new NotImplementedException();
        }
        public async Task Recreate()
        {
            throw new NotImplementedException();
        }

        public string GetDatabaseName()
        {
            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }

}
