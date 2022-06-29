using System.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreSB.Domain.Currency.Validation;
using CoreSB.Infrastructure.IO.Logging;
using CoreSB.Infrastructure.IO.Serialization;
using CoreSB.Universal.Framework;
using InfrastructureCheckers;
using LINQtoObjectsCheck;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSB.Universal.Registrations.IoC
{
    /*Build in logging*/
    public static partial class AutofacConfig
    {
        static ContainerBuilder autofacContainer;

        public static void config(IServiceCollection services)
        {
            /*Autofac autofacContainer */
            autofacContainer = new ContainerBuilder();

            /*Autofac registrations */
            autofacContainer.Populate(services);
        }

        public static ContainerBuilder ConfigureAutofac(IServiceCollection services)
        {
            autofacContainer.RegisterType<ValidatorCustom>().As<IValidatorCustom>();
            autofacContainer.RegisterType<LoggerCustom>().As<ILoggerCustom>();
            autofacContainer.RegisterType<JSONNewtonsoft>().As<ISerialization>();

            return autofacContainer;
        }

        public static ContainerBuilder GetContainer()
        {
            return autofacContainer;
        }
    }
}
