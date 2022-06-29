using CoreSB.Universal;
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

            services.AddScoped<ISerialization, JSONnet>();

        }
    }
}
