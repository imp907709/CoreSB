﻿

/// <summary>
/// Repository domain interface
/// used by interface repositories for DB specific type operations
/// </summary>
namespace crmvcsb.Domain.Universal.IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
            where T : class;

        void Add<T>(T item) where T : class;
        void AddRange<T>(IList<T> items) where T : class;
        void Delete<T>(T item) where T : class;
        void DeleteRange<T>(IList<T> items) where T : class;
        void Update<T>(T item) where T : class;
        void UpdateRange<T>(IList<T> items) where T : class;
        IQueryable<T> SkipTake<T>(int skip, int take)
            where T : class;
        IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class;
        void Save();

        void SaveIdentity(string command);
        void SaveIdentity<T>() where T : class;


        string GetConnectionString();
    }

}
