

using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Domain.Currency.EF
{
    public class RepositoryCurrencyRead : RepositoryEF, IRepositoryEFRead, IRepository
    {
        public RepositoryCurrencyRead(DbContext context) : base(context)
        {

        }
    }
    public class RepositoryCurrencyWrite : RepositoryEF, IRepositoryEFWrite, IRepository
    {
        public RepositoryCurrencyWrite(DbContext context) : base(context)
        {

        }

    }

}
