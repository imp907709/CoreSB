using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Universal
{
    using AutoMapper;
    using CoreSB.Universal.Infrastructure;

    public class Service : IService
    {
        IRepository _repositoryRead;
        IRepository _repositoryWrite;

        internal IMapper _mapper { get; set; }
        internal IValidatorCustom _validator;
        internal ILoggerCustom _logger;

        public IServiceStatus _status { get; set; }

        //public ServiceStatus _status { get { return _status; } set { status = value; } }

        public string actualStatus { get; set; }


        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper = null,
            IValidatorCustom validator = null, ILoggerCustom logger = null)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;

            _status = new ServiceStatus() {Message = StartupConfig.Variables.ServiceInited};
        }

        public Service(IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null,
            ILoggerCustom logger = null)
        {
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;

            _status = new ServiceStatus();
        }

        public async Task<int> AddOne<T>(T item)
            where T : EntityIntIdDAL
        {
            _repositoryWrite.Add(item);
            await _repositoryWrite.SaveAsync();

            return item.Id;
        }

        public async Task<T> GetById<T>(int id)
            where T : EntityIntIdDAL
        {
            return await _repositoryRead.QueryByFilter<T>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter)
            where T : EntityIntIdDAL
        {
            return _repositoryRead.QueryByFilter(filter);
        }

        public async Task Delete<T>(int id)
            where T : EntityIntIdDAL
        {
            var item = await GetById<T>(id);
            _repositoryWrite.Delete(item);
        }

        public async Task Delete<T>(ICollection<int> ids)
            where T : EntityIntIdDAL
        {
            var items = await Filter<T>(s => ids.Contains(s.Id)).ToListAsync();
            _repositoryWrite.DeleteRange(items);
        }


        public enum DateComparisonRange
        {
            Year, Month, Day, Hour
        }

        public enum DateComparisonDirection
        {
            GT, LT, EQ, GTEQ, LTEQ
        }

        public Expression<Func<IDateEntityDAL, bool>> CompareByDateExp(DateTime date, ExpressionType direction,
            DateComparisonRange compareBy)
        {
            var dateRanged = compareBy switch
            {
                DateComparisonRange.Day => new DateTime(date.Year, date.Month,date.Day),
                DateComparisonRange.Month => new DateTime(date.Year, date.Month, 1),
                DateComparisonRange.Year => new DateTime(date.Year, 1, 1),
                DateComparisonRange.Hour => new DateTime(date.Year, date.Month,date.Day, date.Hour,0,0),
                _ => date
            };

            Expression<Func<IDateEntityDAL, bool>>  exp = direction switch
            {
                ExpressionType.Equal => (s) => s.Date == dateRanged,
                ExpressionType.GreaterThanOrEqual => (s) => s.Date >= dateRanged,
                ExpressionType.LessThanOrEqual => (s) => s.Date <= dateRanged,
                ExpressionType.LessThan => (s) => s.Date < dateRanged,
                ExpressionType.GreaterThan => (s) => s.Date > dateRanged,
                _ => (s) => s.Date == dateRanged
            };

            return exp;
        }


        public IRepository GetRepositoryRead()
        {
            return this._repositoryRead;
        }

        public IRepository GetRepositoryWrite()
        {
            return this._repositoryWrite;
        }

        internal void statusChangeAndLog(IServiceStatus newStatus, string message)
        {
            this._status = newStatus;
            this._status.Message = message;
            _logger.Information(this._status.Message);
        }
    }

    public class ServiceStatus : IServiceStatus
    {
        public string Message { get; set; }
    }

    public class Success : ServiceStatus
    {
    }

    public class Failure : ServiceStatus
    {
    }

    public class Error : ServiceStatus
    {
    }

    public class Info : ServiceStatus
    {
    }

    public class OK : ServiceStatus
    {
    }
}
