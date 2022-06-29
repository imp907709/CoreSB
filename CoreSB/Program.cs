using System.Threading.Tasks;
using InfrastructureCheckers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Overall;

namespace CoreSB
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await SQLrepositoriesCheck.GO();
            await Task.Delay(1);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var d = 0.1m;
                    webBuilder.UseStartup<Startup>();
                });
    }
}
