using System.Threading.Tasks;
using KATAS;
using LINQtoObjectsCheck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreSB
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Miscellaneous.InsertionSortInt.GO();
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
