

namespace CoreSB.Infrastructure.InMemmory
{

    using System.IO;
    using Microsoft.Extensions.Configuration;
    public class InMemmoryDbInitializer
    {
        public static IConfigurationRoot configuration { get; set; }

        public static void Initialize()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("CostControlDb");

        }
    }
}
