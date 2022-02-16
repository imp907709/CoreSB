using System.Threading.Tasks;
using coreSB;
using InfrastructureCheckers;
using KATAS;
using LINQtoObjectsCheck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreSB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            KATAS.Miscellaneous.Etna.GO();
            
            LinqCheck.GO();

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
