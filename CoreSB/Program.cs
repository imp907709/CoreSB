using System.Threading.Tasks;
using InfrastructureCheckers;
using LINQtoObjectsCheck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Overall;

namespace CoreSB
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Algorithms.AlgCheck.GO();    
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
