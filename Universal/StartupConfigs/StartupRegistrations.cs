using AutoMapper;
using CoreSB.Domain.Currency.Mapping;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace aspnetcoreapp.Universal.StartupConfigs
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
    }
}