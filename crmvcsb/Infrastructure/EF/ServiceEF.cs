﻿

namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Threading.Tasks;
    using crmvcsb.Universal.Infrastructure;

    public class ServiceEF : Service, IService, IServiceEF
    {
        IRepository _repositoryRead;
        IRepository _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;

        public ServiceEF(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null)
            : base(repositoryRead, repositoryWrite, mapper, validator)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
        public ServiceEF(IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null)
            : base(repositoryWrite, mapper, validator)
        {
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<EntityEntry<T>> AddAsync<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

      
        public DatabaseFacade GetDatabase()
        {
            throw new NotImplementedException();
        }

        public DbContext GetEFContext()
        {
            throw new NotImplementedException();
        }
       
        public void SaveIdentity(string command)
        {
            throw new NotImplementedException();
        }

        public void SaveIdentity<T>() where T : class
        {
            throw new NotImplementedException();
        }

      
    }
}
