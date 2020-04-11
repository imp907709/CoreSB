using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace crmvcsb.Domain.Interfaces
{
    using crmvcsb.Domain.TestModels;

    public interface ICQRScrud
    {
        T Add<T>(T item) where T : class;
        void AddRange<T>(IList<T> items) where T : class;
        void Delete<T>(T item) where T : class;
        void DeleteRange<T>(IList<T> items) where T : class;
        T Update<T>(T item) where T : class;
        void UpdateRange<T>(IList<T> items) where T : class;
        IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class;
    }

    public interface ICQRSBloggingWrite
    {
        PostAPI PersonAdsPostToBlog(PersonAdsPostCommand command);
        bool PersonDeletesPostFromBlog(PersonDeletesPost command);
        PostAPI PersonUpdatesPost(PersonUpdatesBlog command);
    }
    public interface ICQRSBloggingRead
    {
        IList<PostAPI> Get(GetPostsByPerson command);
        IList<PostAPI> Get(GetPostsByBlog command);
        IList<BlogAPI> Get(GetBlogsByPerson command);
    }
    
}
