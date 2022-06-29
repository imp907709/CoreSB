using Autofac;
using AutoMapper;
using CoreSB.Domain.Currency;
using CoreSB.Domain.Currency.EF;
using CoreSB.Domain.Currency.Mapping;
using CoreSB.Domain.NewOrder;
using CoreSB.Domain.NewOrder.EF;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Registrations;
using CoreSB.Universal.Registrations.IoC;
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
            
            services.RegisterSwagger();
            
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

            Configurations.RegisterSQL(services, Configuration);

            RegistrationsIoC.ConfigureMainServices(services);
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
    }
}
