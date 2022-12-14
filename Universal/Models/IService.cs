
using System;
using System.Linq.Expressions;
using AutoMapper;

namespace CoreSB.Universal
{


    public interface IService
    {
        Expression<Func<IDateEntityDAL, bool>> CompareByDateExp(DateTime date, ExpressionType direction,
            Service.DateComparisonRange compareBy);
        
        IRepository GetRepositoryRead();
        IRepository GetRepositoryWrite();

        string actualStatus { get; }

        IServiceStatus _status { get; }

    }

    public interface IServiceStatus
    {
        public string Message { get; set; }
    }


}
