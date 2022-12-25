using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreSB.Universal.Infrastructure.EF
{
    public interface IServiceEF : IService
    {
        /**
 > take page from read
	
        > for all pages from write
        > take page from write, eclude exists
	
        > remove from read
	
//add
        > take page from write
	
        > for all pages from read
        > exluce existed
		
        > add remained 

//update
        > for each page from write
	
        > for each page from read
        > by id compare	
        > update

//update by dates 	
        > narrow by date //update

// update by property
        > compare by //update        *
         */
        Task Sync<T>(string entityName)
            where T: EntityIntIdDAL;
        

        Task<int> AddOne<T>(T item)
            where T : EntityIntIdDAL;

        Task<T> GetById<T>(int id)
            where T : EntityIntIdDAL;

        IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter)
            where T : EntityIntIdDAL;

        Task Delete<T>(int id)
            where T : EntityIntIdDAL;

        Task Delete<T>(ICollection<int> ids)
            where T : EntityIntIdDAL;
        
        
        public Expression<Func<IDateEntityDAL, bool>> CompareByDateExp(DateTime date, ExpressionType direction,
            Service.DateComparisonRange compareBy);

    }
}
