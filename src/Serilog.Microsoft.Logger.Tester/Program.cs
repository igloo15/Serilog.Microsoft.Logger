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

            LoggerFactory factory = new LoggerFactory();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var section = config.GetSection("Logging:File");

            var hb = new HostBuilder()
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConfiguration(config.GetSection("Logging"));

                    builder.AddFile();
                    builder.AddSerilogConsole();
                });

            var host = hb.Build();

            factory.AddFile(section);
            factory.AddSerilogConsole(config.GetSection("Logging:SerilogConsole"));

            var logger = factory.CreateLogger("Program");

            for(int i = 0; i < 1000; i++)
            {
                logger.LogWarning("Test"+i+" {eventId} on template {template}", i, config.GetValue<string>("Logging:File:PathFormat"));
            }

            Console.ReadLine();

            factory.Dispose();
        }
    }
}
