using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;

namespace CoreSB.Domain.NewOrder.EF
{
    public class NewOrderServiceEF : Service, INewOrderServiceEF
    {
        IRepositoryEF _repositoryRead;
        IRepositoryEF _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;

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

        public void ReInitialize()
        {

            _repositoryWrite.ReInitialize();
            _repositoryRead?.ReInitialize();
        }
        
        public void ReGenerate()
        {

            _repositoryWrite.ReInitialize();
            List<AddressDAL> addresses = new List<AddressDAL>();

            for (int i = 0; i < 10; i++)
            {
                addresses.Add(new AddressDAL() { Id = i + 1, StreetName = $"test street {i}", Code = i + 1 });
            };

            _repositoryWrite.AddRange(addresses);

            try
            {
                _repositoryWrite.SaveIdentity("Adresses");
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void CleanUp()
        {
            _repositoryWrite.CleanUp();
            var addressesExist = _repositoryWrite.QueryByFilter<AddressDAL>(s => s.Id != 0).ToList();
            _repositoryWrite.DeleteRange(addressesExist);
            try { _repositoryWrite.Save(); } catch (Exception) { throw; }
        }

    }
}
