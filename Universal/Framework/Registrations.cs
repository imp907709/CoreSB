using CoreSB.Domain.Currency.Validation;
using CoreSB.Infrastructure.IO.Logging;
using CoreSB.Infrastructure.IO.Serialization;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using CoreSB.Universal.Infrastructure.IO.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSB
{
    public static class RegistrationsIoC
    {
        public static void ConfigureMainServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            
            services.AddScoped<IService, Service>();
            
            services.AddScoped<IRepositoryEF, RepositoryEF>();
            services.AddScoped<IServiceEF, ServiceEF>();
            
            services.AddScoped<ISerialization, JSONnet>();

            services.AddScoped<IValidatorCustom,ValidatorCustom>();
            services.AddScoped<ILoggerCustom,LoggerCustom>();
            services.AddScoped<ISerialization,JSONNewtonsoft>();
        }
    }
}
