
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreSB.Universal.Infrastructure.EF
{
    /// <summary>
    /// Basic repository implementation
    /// </summary>
    public class RepositoryEF : IRepositoryEF
    {
        DbContext _context;

        public DbContext GetContext()
        {
            return this._context;
        }

        public RepositoryEF(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
            where T : class
        {
            return (expression == null)
                ? this._context.Set<T>()
                : this._context.Set<T>().Where(expression);
        }

        public void Add<T>(T item)
            where T : class
        {
            this._context.Set<T>().Add(item);
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T item)
           where T : class
        {
            return await this._context.Set<T>().AddAsync(item);
        }

        public void AddRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().AddRange(items);
        }

        public Task AddRangeAsync<T>(IList<T> items)
                where T : class
        {
            return this._context.Set<T>().AddRangeAsync(items);
        }

        public void Delete<T>(T item)
           where T : class
        {
            this._context.Set<T>().Remove(item);
        }

        public void DeleteRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().RemoveRange(items);
        }

        public void Update<T>(T item)
            where T : class
        {
            this._context.Set<T>().Update(item);
        }

        public void UpdateRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().UpdateRange(items);
        }

        public IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class
        {

            return this._context.Set<T>().Where(expression);
        }

        public IQueryable<T> SkipTake<T>(int skip = 0, int take = 10)
            where T : class
        {
            return this._context.Set<T>().Skip(skip).Take(take);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public string GetConnectionString()
        {
            return this._context.Database.GetDbConnection().ConnectionString;
        }
        public string GetDatabaseName()
        {
            return this._context.Database.GetDbConnection().ConnectionString;
        }

        public async Task DropDB()
        {
            await this._context.Database.EnsureDeletedAsync();
        }
        
        public async Task CreateDB()
        {
            await this._context.Database.EnsureCreatedAsync();
        }
        
        public void ReInitialize()
        {
            this._context.Database.EnsureDeleted();
            this._context.Database.EnsureCreated();
        }

        public void CleanUp()
        {
            this._context.Database.EnsureDeleted();
            this._context.Database.EnsureCreated();
        }

        public void SaveIdentity<T>()
            where T : class
        {
            var set = this._context.Set<T>();
            var prop = this._context.GetType().GetProperties();
            var t_ = typeof(T);

            PropertyInfo p = prop.Where(s => s.PropertyType.GenericTypeArguments[0].Equals(typeof(T))).FirstOrDefault();

            if (p != null)
            {
                this.SaveIdentity(p.Name);
            }
        }
        
        
        public async Task<int> QueryRaw(string sqlRaw)
        {
              return await this._context.Database.ExecuteSqlRawAsync(sqlRaw);
        }
        
        /*Provides identity column manual insert while testing */
        public void SaveIdentity(string tableFullName)
        {
            string cmd = $"SET IDENTITY_INSERT {tableFullName} ON;";
            this._context.Database.OpenConnection();
            this._context.Database.ExecuteSqlRaw(cmd);
            this._context.SaveChanges();
            cmd = $"SET IDENTITY_INSERT {tableFullName} OFF;";
            this._context.Database.ExecuteSqlRaw(cmd);
            this._context.Database.CloseConnection();
        }





        public DatabaseFacade GetDatabase()
        {
            return this._context.Database;
        }

        public DbContext GetEFContext()
        {
            if (this._context is DbContext context)
            {
                return context;
            }
            return null;
        }
    }

}
