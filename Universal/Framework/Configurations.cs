using Autofac;
using AutoMapper;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.EF;
using CoreSB.Domain.NewOrder;
using CoreSB.Domain.NewOrder.EF;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Registrations.IoC;
using CoreSB.Universal.StartupConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CoreSB.Universal.Registrations
{
    public static class Configurations
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreSB", Version = "v1" });
            });
        }
        
        public static void RegisterSQL(IServiceCollection services, IConfiguration configuration)
        {
            //If config is SQL scope
            if (configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType ==
                ContextType.SQL)
            {
                //MS SQL connection exist
                if (!string.IsNullOrEmpty(configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>()
                    .MsSQlCoreSBConnection))
                {
                    ConfigureAutofacDbContexts(services, AutofacConfig.GetContainer(), configuration);
                }
            }
        }
        
         /* Db context registration with sql server for connection strings from appsettings.json */
        public static ContainerBuilder ConfigureAutofacDbContexts(IServiceCollection services, ContainerBuilder autofacContainer, IConfiguration configuration)
        {

            /*
            * Registering multiple IRepository clones with different connections trings
            * For multiple SQL DBs in one project
            */
            var connection = configuration
                .GetSection(Variables.ConnectionStrings)
                .Get<ConnectionStrings>()
                .MsSQlCoreSBConnection;
            if (!string.IsNullOrEmpty(connection))
            {
                autofacContainer.RegisterType<RepositoryNewOrderRead>()
                    .WithParameter(Variables.context,
                        new ContextNewOrderRead(new DbContextOptionsBuilder<ContextNewOrderRead>()
                            .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
                    .As<IRepositoryEFRead>().AsSelf()
                    .InstancePerLifetimeScope();
            }
            
            ////--------
          
            autofacContainer.RegisterType<RepositoryNewOrderWrite>()
            .WithParameter(Variables.context,
                new ContextNewOrderWrite(new DbContextOptionsBuilder<ContextNewOrderWrite>()
                .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            .As<IRepositoryEFWrite>().AsSelf()
            .InstancePerLifetimeScope();
            
            autofacContainer.RegisterType<RepositoryCurrencyRead>()
            .WithParameter(Variables.context,
                new CurrencyContextRead(new DbContextOptionsBuilder<CurrencyContextRead>()
                .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            .As<IRepositoryEFRead>().AsSelf()
            .InstancePerLifetimeScope();
            autofacContainer.RegisterType<RepositoryCurrencyWrite>()
            .WithParameter(Variables.context,
                new CurrencyContextWrite(new DbContextOptionsBuilder<CurrencyContextWrite>()
                .UseSqlServer(configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            .As<IRepositoryEFWrite>().AsSelf()
            .InstancePerLifetimeScope();

            autofacContainer.Register(ctx => new NewOrderServiceEF(
                ctx.Resolve<RepositoryNewOrderRead>(),
                ctx.Resolve<RepositoryNewOrderWrite>(),
                ctx.Resolve<IMapper>(),
                ctx.Resolve<IValidatorCustom>()
                ))
            .As<INewOrderServiceEF>()
            .AsSelf()
            .InstancePerLifetimeScope();

            autofacContainer.Register(ctx => new CurrencyServiceEF(
                ctx.Resolve<RepositoryCurrencyRead>(),
                ctx.Resolve<RepositoryCurrencyWrite>(),
                ctx.Resolve<IMapper>(),
                ctx.Resolve<IValidatorCustom>(),
                ctx.Resolve<ILoggerCustom>()
                ))
            .As<ICurrencyServiceEF>()
            .AsSelf()
            .InstancePerLifetimeScope();

            return autofacContainer;
        }
    }
    
}
