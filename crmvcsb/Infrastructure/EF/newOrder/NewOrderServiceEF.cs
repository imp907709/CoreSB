
namespace crmvcsb.Infrastructure.EF.NewOrder
{
    using AutoMapper;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal.DomainSpecific.NewOrder.DAL;
    using crmvcsb.Universal.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class NewOrderServiceEF : ServiceEF, INewOrderServiceEF
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
        public override void ReInitialize()
        {

            _repositoryWrite.ReInitialize();
            _repositoryRead.ReInitialize();

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
        public override void CleanUp()
        {
            _repositoryWrite.CleanUp();
            var addressesExist = _repositoryWrite.QueryByFilter<AddressDAL>(s => s.Id != 0).ToList();
            _repositoryWrite.DeleteRange(addressesExist);
            try { _repositoryWrite.Save(); } catch (Exception) { throw; }           
        }
    
    }
}
