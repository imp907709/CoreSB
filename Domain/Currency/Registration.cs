using System.Drawing;
using CoreSB.Domain.Currency.EF;
using CoreSB.Domain.Currency.Mongo;
using CoreSB.Universal;
using CoreSB.Universal.Framework;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Infrastructure.Mongo;
using CoreSB.Universal.StartupConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CoreSB.Domain.Currency
{
    public class Registration
    {
        public static void ConfigureCurrencyServices(IServiceCollection services, IConfiguration configuration)
        {
            
            // autofacContainer.RegisterType<RepositoryCurrencyRead>()
            //     .WithParameter(Variables.context,
            //         new CurrencyContextRead(new DbContextOptionsBuilder<CurrencyContextRead>()
            //             .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            //     .As<IRepositoryEFRead>().AsSelf()
            //     .InstancePerLifetimeScope();
            // autofacContainer.RegisterType<RepositoryCurrencyWrite>()
            //     .WithParameter(Variables.context,
            //         new CurrencyContextWrite(new DbContextOptionsBuilder<CurrencyContextWrite>()
            //             .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            //     .As<IRepositoryEFWrite>().AsSelf()
            //     .InstancePerLifetimeScope();

            services.AddDbContext<CurrencyContextWrite>(o => o.UseSqlServer(
                configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)));
            services.AddDbContext<CurrencyContextRead>(o => o.UseSqlServer(
                configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)));
            
            services.AddScoped<DbContext, CurrencyContextWrite>();

            services.AddScoped<IRepositoryEFWrite, RepositoryCurrencyWrite>();
            services.AddScoped<IRepositoryEFRead, RepositoryCurrencyRead>();
            services.AddScoped<ICurrencyServiceEF, CurrencyServiceEF>();

            
            //mongo services
            var option = getMongoOption(configuration);
            services.AddScoped<IMongoContext>(s=> 
                new MongoContext(option.ConnectionString, option.DatabaseName));
            services.AddScoped<ICurrencyMongoContext>(s=> 
                new CurrencyMongoContext(option.ConnectionString, option.DatabaseName));
            
            services.AddScoped<IMongoService, MongoService>();
            services.AddScoped<ICurrencyMongoService, CurrencyMongoService>();

        }

        static MongoOption getMongoOption(IConfiguration conf)
        {
            var option = new MongoOption();
            option = conf.GetSection(option.ConfigString).Get<MongoOption>();
            return option;
        }
    }
}
