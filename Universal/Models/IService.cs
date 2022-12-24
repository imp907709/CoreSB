
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

namespace CoreSB.Universal
{


    public interface IService
    {
        public Expression<Func<IDateEntityDAL, bool>> CompareByDateExp(DateTime date, ExpressionType direction,
            Service.DateComparisonRange compareBy);

        public IRepository GetRepositoryRead();
        public IRepository GetRepositoryWrite();

        public Task DropDB();
        public Task CreateDB();

        public string GetConnectionString();
        
        public string actualStatus { get; }

        public IServiceStatus _status { get; }

    }

    public interface IServiceStatus
    {
        public string Message { get; set; }
    }


}
