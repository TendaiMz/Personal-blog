using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Blog.Service
{    
    public class Program
    {
        public static void Main(string[] args)
        {           
            try
            {
                Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console().CreateLogger();

                Log.Information("Application starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext,appConfiguration)=> {
                    appConfiguration.SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: false);   
                })
                .UseSerilog((hostingContext, loggerConfiguration) => {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                                       .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                                       .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
