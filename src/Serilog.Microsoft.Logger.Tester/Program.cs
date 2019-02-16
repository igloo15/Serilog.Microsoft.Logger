using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace Serilog.Microsoft.Logger.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            var hb = new HostBuilder()
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConfiguration(config.GetSection("Logging"));

                    builder.AddFile();
                    builder.AddSerilogConsole();
                });

            var host = hb.Build();

            var anotherLogger = host.Services.GetService(typeof(ILogger<Program>)) as ILogger<Program>;

            anotherLogger.LogWarning("Does this work");

            
            for(int i = 0; i < 1000; i++)
            {
                anotherLogger.LogWarning("Test"+i+" {eventId} on template {template}", i, config.GetValue<string>("Logging:File:PathFormat"));
            }

            Console.ReadLine();
            host.Dispose();
        }
    }
}
