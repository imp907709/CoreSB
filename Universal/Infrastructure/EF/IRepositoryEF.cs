


using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace CoreSB.Universal.Infrastructure.EF
{
    public interface IRepositoryEF : IRepository
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
            where T : class;

        Task<int> QueryRaw(string sqlRaw);
        void SaveIdentity(string command);
        void SaveIdentity<T>() where T : class;

        Task<EntityEntry<T>> AddAsync<T>(T item) where T : class;

        DatabaseFacade GetDatabase();
        DbContext GetEFContext();
    }

    public interface IRepositoryEFRead : IRepositoryEF { }
    public interface IRepositoryEFWrite : IRepositoryEF { }
}
