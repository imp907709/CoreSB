using AutoMapper;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Domain.Logging.EF
{
    public class LoggingServiceEF : ServiceEF, ILoggingServiceEF
    {
        public LoggingServiceEF(IRepositoryEF repositoryWrite, IMapper mapper,
            IValidatorCustom validator, ILoggerCustom logger)
            : base(repositoryWrite, mapper, validator, logger)
        {
            _mapper = mapper;
            _validator = validator;
        }
    }
}
