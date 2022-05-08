using System.Threading.Tasks;
using LINQtoObjectsCheck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreSB
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //await Miscellaneous.Miscellaneous.Check.GO_async();
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
