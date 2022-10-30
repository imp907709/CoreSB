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
            // LINQcheck.GO();
            
            // await SQLrepositoriesCheck.GO();
            // await Task.Delay(1);
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
