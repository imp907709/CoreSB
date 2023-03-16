using CoreSB.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Domain.Logging.EF
{
    public class LoggingRepository : RepositoryEF, IRepositoryEF
    {
        public LoggingRepository(DbContext context) : base(context)
        {
        }
    }
}
