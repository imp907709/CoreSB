
namespace CoreSB.Universal
{
    using AutoMapper;
    using CoreSB.Universal.Infrastructure;

    public class Service : IService
    {

        IRepository _repositoryRead;
        IRepository _repositoryWrite;

        IMapper _mapper;
        IValidatorCustom _validator;
        ILoggerCustom _logger;

        public IServiceStatus _status { get; set; }
        //public ServiceStatus _status { get { return _status; } set { status = value; } }

        public string actualStatus { get; set; }


        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;

            _status = new ServiceStatus(){ Message = StartupConfig.Variables.ServiceInited };

        }
        public Service(IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
        {
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;

            _status = new ServiceStatus();
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

    public class Success : ServiceStatus { }
    public class Failure : ServiceStatus { }
    public class Error : ServiceStatus { }
    public class Info : ServiceStatus { }
    public class OK : ServiceStatus { }

}
