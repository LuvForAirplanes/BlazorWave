using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BlazorWave.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorWave
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();
        // }

        public static IHost WebHostInstance;
        public static void Main(string[] args)
        {
            try
            {
                var isService = !(Debugger.IsAttached || args.Contains("--console") || args.Contains("-c"));
                var builder = CreateHostBuilder(args.Where(arg => arg != "--console" && arg != "-c").ToArray());

                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    builder.UseContentRoot(pathToContentRoot);
                }

                WebHostInstance = builder.Build();

                var scope = WebHostInstance.Services.CreateScope();

                var seedDataService = scope.ServiceProvider.GetService<SeedDataService>();
                seedDataService.SeedPeriodsAsync(); //Don't hold up the startup process. Seeding will catch up later.

                // if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                //     WebHostInstance.RunAsService();
                // else
                WebHostInstance.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:32981");
                });
    }
}
