using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Domain.NewOrder.EF
{
    public class RepositoryNewOrder : RepositoryEF, IRepositoryEF, IRepository
    {
        public RepositoryNewOrder(DbContext context) : base(context)
        {

        }
    }

    public class RepositoryNewOrderRead : RepositoryNewOrder, IRepositoryEFRead
    {
        public RepositoryNewOrderRead(ContextNewOrderRead context) : base(context)
        {

        }
    }
    public class RepositoryNewOrderWrite : RepositoryNewOrder, IRepositoryEFWrite
    {
        public RepositoryNewOrderWrite(ContextNewOrderWrite context) : base(context)
        {

        }
    }
}
