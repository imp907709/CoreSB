using Autofac;
using AutoMapper;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.EF;
using CoreSB.Domain.Currency.Mapping;
using CoreSB.Domain.NewOrder;
using CoreSB.Domain.NewOrder.EF;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Infrastructure.IoC;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StartupConfig;

namespace CoreSB
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreSB", Version = "v1" });
            });
            
            /*SignalR registration*/
            services.AddSignalR();
            
            /*Automapper Register */
            services.AddAutoMapper(typeof(Startup));
            
            /*Mapper initialize with Instance API initialization */
            var config = ConfigureAutoMapper();
            IMapper mapper = new Mapper(config);
            
            /*Autofac registrations */
            //autofacContainer.Populate(services);
            AutofacConfig.config(services);

            /*Registration of automapper with autofac Instance API */
            //autofacContainer.RegisterInstance(mapper).As<IMapper>();
            AutofacConfig.GetContainer().RegisterInstance(mapper).As<IMapper>();
            
            AutofacConfig.ConfigureAutofac(services);

            //If config is SQL scope
            if (Configuration.GetSection("RegistrationSettings").Get<RegistrationSettings>().ContextType ==
                ContextType.SQL)
            {
                //MS SQL conenciton exist
                if (!string.IsNullOrEmpty(Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>()
                    .MsSQlCoreSBConnection))
                {
                    ConfigureAutofacDbContexts(services, AutofacConfig.GetContainer());
                }
            }

            ConfigureFluentValidation(services);

            ConfigureMainServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreSB v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        public MapperConfiguration ConfigureAutoMapper()
        {
            return CurrenciesMapping.config();
        }
        
        public void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
        }
        
         /* Db context registration with sql server for connection strings from appsettings.json */
        public ContainerBuilder ConfigureAutofacDbContexts(IServiceCollection services, ContainerBuilder autofacContainer)
        {

            /*
            * Registering multiple IRepository clones with different connections trings
            * For multiple SQL DBs in one project
            */
            var connection = Configuration.GetSection(Variables.ConnectionStrings)
                .Get<ConnectionStrings>()
                .MsSQlCoreSBConnection;
            if (!string.IsNullOrEmpty(connection))
            {
                autofacContainer.RegisterType<RepositoryNewOrderRead>()
                    .WithParameter(Variables.context,
                        new ContextNewOrderRead(new DbContextOptionsBuilder<ContextNewOrderRead>()
                            .UseSqlServer(Configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
                    .As<IRepositoryEFRead>().AsSelf()
                    .InstancePerLifetimeScope();
            }
            
            ////--------
          
            autofacContainer.RegisterType<RepositoryNewOrderWrite>()
            .WithParameter(Variables.context,
                new ContextNewOrderWrite(new DbContextOptionsBuilder<ContextNewOrderWrite>()
                .UseSqlServer(Configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            .As<IRepositoryEFWrite>().AsSelf()
            .InstancePerLifetimeScope();
            
            autofacContainer.RegisterType<RepositoryCurrencyRead>()
            .WithParameter(Variables.context,
                new CurrencyContextRead(new DbContextOptionsBuilder<CurrencyContextRead>()
                .UseSqlServer(Configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
            .As<IRepositoryEFRead>().AsSelf()
            .InstancePerLifetimeScope();
            autofacContainer.RegisterType<RepositoryCurrencyWrite>()
            .WithParameter(Variables.context,
                new CurrencyContextWrite(new DbContextOptionsBuilder<CurrencyContextWrite>()
                .UseSqlServer(Configuration.GetConnectionString(Variables.MsSQlCoreSBConnection)).Options))
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

        public void ConfigureMainServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IService, Service>();
        }
    }
}
