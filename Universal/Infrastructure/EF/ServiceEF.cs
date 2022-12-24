using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Universal.Infrastructure.EF
{
    public class ServiceEF : Service, IServiceEF
    {
        private IRepositoryEF _repositoryRead;
        private IRepositoryEF _repositoryWrite;
        
        public ServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite, IMapper mapper = null,
            IValidatorCustom validator = null, ILoggerCustom logger = null) : base(repositoryRead, repositoryWrite,
            mapper, validator, logger)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
        }

        public ServiceEF(IRepositoryEF repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null,
            ILoggerCustom logger = null) : base(repositoryWrite, mapper, validator, logger)
        {
            _repositoryWrite = repositoryWrite;
        }
        
        
        /// <summary>
        /// Temporary naive synchronization by paging, simpky by ID
        /// later more complex logic with dates, properties
        /// sync token
        /// </summary>
        //sync alg
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
       public async Task Sync<T>(string entityName)
            where T: EntityIntIdDAL
        {
            var maxRead = await _repositoryWrite.QueryRaw($"select count(*) from dbo.{entityName}");

            var page = 0;
            var toRemove = new List<T>();
            var gap = 1000;
            for (int i = 0; i < maxRead / 100; i++)
            {
                if (page >= gap)
                {
                    _repositoryRead.DeleteRange(toRemove);
                    await _repositoryRead.SaveAsync();
                    gap += 1000;
                }
                
                var read = await _repositoryRead
                    .QueryByFilter<T>(s => s.Id != 0).Skip(page).Take(page+100).ToListAsync();

                var r = await _repositoryWrite
                    .QueryByFilter<T>(s => !read.Select(c => c.Id).Contains(s.Id)).ToListAsync();
                
                if(r?.Any() == true)
                    toRemove.AddRange(r);

                page += 100;
            }
            
         
        }
    }
}
