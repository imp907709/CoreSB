using CoreSB.Domain.Currency.EF;
using CoreSB.Domain.Logging;
using CoreSB.Domain.Logging.EF;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.StartupConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSB.Domain
{
    public partial class Registration
    {
        public static void ConfigureServicesLogging(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<LoggingContext>(o => o.UseSqlServer(
                configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)));
            services.AddDbContext<CurrencyContextRead>(o => o.UseSqlServer(
                configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)));
            
            services.AddScoped<DbContext, LoggingContext>();

            services.AddScoped<IRepositoryEF, LoggingRepository>();

            services.AddScoped<ILoggingServiceEF, LoggingServiceEF>();
            
        }

    }
}
