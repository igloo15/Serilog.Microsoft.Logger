using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Serilog.Microsoft.Logger.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            LoggerFactory factory = new LoggerFactory();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var section = config.GetSection("Logging:File");

            factory.AddFile(section);
            factory.AddSerilogConsole(config.GetSection("Logging:Console"));

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
