using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Domain.NewOrder.EF
{
    public class NewOrderServiceEF : Service, INewOrderServiceEF
    {
        IRepositoryEF _repositoryRead;
        IRepositoryEF _repositoryWrite;

        public NewOrderServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite, IMapper mapper, IValidatorCustom validator)
           : base(repositoryRead, repositoryWrite, mapper, validator)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
        public NewOrderServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite, IMapper mapper)
            : base(repositoryRead, repositoryWrite, mapper)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
        }
        public NewOrderServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite)
             : base(repositoryRead, repositoryWrite)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
        }
        public NewOrderServiceEF(IRepositoryEF repositoryWrite)
            : base(repositoryWrite)
        {
            _repositoryWrite = repositoryWrite;
        }
        
        public async Task ReInitialize()
        {
            await _repositoryWrite.DropDB();
            await _repositoryWrite.CreateDB();
        }
    }
}
