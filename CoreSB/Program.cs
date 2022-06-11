using System.Threading.Tasks;
using KATAS;
using LINQtoObjectsCheck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreSB
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            KATAS.BracketsChecker.GO();
            //await KATAS.HTTPserializeSave.GO();
            //NetPlatformCheckers.ExtensionsDIY.GO();
            //NetPlatformCheckers.StringObjectEquality.GO();
            //LINQtoObjectsCheck.LinqCheck.GO();
            //Miscellaneous.InsertionSortInt.GO();
            await Task.Delay(1);
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
