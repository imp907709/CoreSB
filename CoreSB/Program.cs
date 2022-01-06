﻿using System.Threading.Tasks;
using InfrastructureCheckers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreSB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await KATAS.Overall.GO_async();
            RepoAndUOWCheck.GO();
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
