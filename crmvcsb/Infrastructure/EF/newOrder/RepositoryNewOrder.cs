
namespace crmvcsb.Infrastructure.EF.NewOrder
{

    using Microsoft.EntityFrameworkCore;
  
    using crmvcsb.Domain.Universal.IRepository;
    using crmvcsb.Infrastructure.EF;

    public class RepositoryNewOrder : RepositoryEF, IRepositoryEF, IRepository
    {
        public RepositoryNewOrder(DbContext context) : base(context)
        {

        }
    }

}