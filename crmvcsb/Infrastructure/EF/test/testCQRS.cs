using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System;

using System.Linq;

using AutoMapper;

using crmvcsb.Domain.Blogging.API;
using crmvcsb.Infrastructure.Blogging.EF;

namespace crmvcsb.Infrastructure.EF
{

    using crmvcsb.Domain.Interfaces;
    using crmvcsb.Domain.IRepository;

    public class CQRSEFBlogging 
    {
        internal IRepository _repository;
        internal IMapper _mapper;
        
        public CQRSEFBlogging(IRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

    }

    public class CQRSBloggingWrite : CQRSEFBlogging, ICQRSBloggingWrite
    {

        public CQRSBloggingWrite(IRepository repository, IMapper mapper) 
            : base(repository,mapper){}

        /*Adding object drom command, mapping and command->EF returning EF -> API*/
        public PostAPI PersonAdsPostToBlog(PersonAdsPostCommand command)
        {
            PostAPI postReturn = new PostAPI();

            if (this._mapper == null || command == null)
            {
                return null;
            }

            try
            {
                var postToAdd = this._mapper.Map(command, command.GetType(), typeof(PostEF)) as PostEF;
                this._repository.Add<PostEF>(postToAdd);
                this._repository.Save();

                var postAdded = this._repository.QueryByFilter<PostEF>(s => s.PostId == postToAdd.PostId)
                .Include(x => x.Blog).Include(x => x.Author)
                .FirstOrDefault();

                postReturn = this._mapper.Map(postAdded, postAdded.GetType(), typeof(PostAPI)) as PostAPI;
                return postReturn;
            }
            catch (Exception e)
            {

            }

            return postReturn;
        }

        public PostAPI PersonUpdatesPost(PersonUpdatesBlog command)
        {
          
            PostAPI updatedItem = new PostAPI();
            if (command == null || command?.Post == null || this._mapper == null) { return updatedItem; }
            try
            {
                PostEF itemToUpdate = this._repository.GetAll<PostEF>(s => s.PostId == command.Post.PostId).FirstOrDefault();
                //itemToUpdate = this._mapper.Map<PostAPI,PostEF>(command.Post);
                
                itemToUpdate.Title = command.Post.Title;
                itemToUpdate.Content = command.Post.Content;

                this._repository.Update<PostEF>(itemToUpdate);
                this._repository.Save();

                var itemExists = this._repository.GetAll<PostEF>(s => s.PostId == command.Post.PostId)
                .Include(s => s.Blog)
                .Include(s => s.Author)
                .FirstOrDefault();

                if (itemExists !=null)
                {
                    updatedItem = this._mapper.Map<PostEF,PostAPI>(itemExists);
                }

                return updatedItem;
            }
            catch (Exception e)
            {

                return null;
            }

        }

        public bool PersonDeletesPostFromBlog(PersonDeletesPost command)
        {
            if (command == null)
            {
                return false;
            }
            try
            {
                var itemToDelete = this._repository.GetAll<PostEF>(s => s.PostId == command.PostId).FirstOrDefault();

                if (itemToDelete != null)
                {
                    this._repository.Delete<PostEF>(itemToDelete);
                    this._repository.Save();
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

    }

    public class CQRSBloggingRead : CQRSEFBlogging, ICQRSBloggingRead
    {

        public CQRSBloggingRead(IRepository repository, IMapper mapper)
            : base(repository, mapper) { }


        public IList<PostAPI> Get(GetPostsByPerson command)
        {
            IList<PostAPI> itemsReturn = new List<PostAPI>();
            try
            {
                var itemsExist = this._repository.GetAll<PostEF>()
                .Where(s => s.AuthorId == command.PersonId)
                .ToList();

                if (itemsExist.Any())
                {
                    itemsReturn = this._mapper.Map<IList<PostEF>, IList<PostAPI>>(itemsExist);
                }
            }
            catch (Exception e)
            {

            }

            return itemsReturn;

        }
        
        public IList<PostAPI> Get(GetPostsByBlog command)
        {
            IList<PostAPI> postsReturn = new List<PostAPI>();
            try
            {
                var postsExist = this._repository.GetAll<PostEF>()
                .Include(s => s.Blog)
                .Where(s => s.BlogId == command.BlogId)
                .ToList();

                if(postsExist.Any())
                {
                    postsReturn = this._mapper.Map<IList<PostEF>,IList<PostAPI>>(postsExist);
                }
            }
            catch (Exception e)
            {

            }

            return postsReturn;
        }
        public IList<BlogAPI> Get(GetBlogsByPerson command)
        {
            IList<BlogAPI> itemsReturn = new List<BlogAPI>();
            try
            {
                var itemsExist = this._repository.GetAll<PostEF>()
                .Include(s => s.Author)
                .Include(s => s.Blog)
                .Where(s => s.Author.Id == command.PersonId)
                .Select(s => s.Blog)
                .ToList();

                if (itemsExist.Any())
                {
                    itemsReturn = this._mapper.Map<IList<BlogEF>, IList<BlogAPI>>(itemsExist);
                }
            }
            catch (Exception e)
            {

            }

            return itemsReturn;
        }
    
    }

   
}
