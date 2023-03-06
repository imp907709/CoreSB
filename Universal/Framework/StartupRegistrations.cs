using AutoMapper;
using CoreSB.Domain.Currency.Mapping;
using CoreSB.Universal.StartupConfigs;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSB.Universal.Framework
{
    public class StartupRegistrations
    {
        public static void ConfigureAutoMapper()
        {
            var cfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrenciesMapping>();
            });
        }
        
        public static void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
        }

        public static void OptionsBinding(IConfiguration config)
        {
            var connstrings = new ConnectionStringsOption();
            var mongo = new MongoOption();
            
            config.GetSection(connstrings.ConfigString).Bind(connstrings);
            config.GetSection(mongo.ConfigString).Bind(mongo);
        }
    }
}
