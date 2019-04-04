using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Microsoft.Logging.Console;
using System;

namespace Microsoft.Extensions.Logging
{
    public static class MicrosoftLoggingExtensions
    {
        public static ILoggerFactory AddSerilogConsole(this ILoggerFactory loggerFactory, IConfigurationSection configuration)
        {
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = configuration.Get<ConsoleConfiguration>();

            return loggerFactory.AddSerilogConsole(config);
        }

        public static ILoggerFactory AddSerilogConsole(this ILoggerFactory loggerFactory, ConsoleConfiguration config)
        {
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var serilog = Utility.CreateConsoleLogger(config);

            return loggerFactory.AddSerilog(serilog);
        }

        public static ILoggingBuilder AddSerilogConsole(this ILoggingBuilder loggingBuilder, IConfigurationSection configuration)
        {
            if (loggingBuilder == null) throw new ArgumentNullException(nameof(loggingBuilder));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = configuration.Get<ConsoleConfiguration>();

            return loggingBuilder.AddSerilogConsole(config);
        }

        public static ILoggingBuilder AddSerilogConsole(this ILoggingBuilder loggingBuilder, ConsoleConfiguration config)
        {
            if (loggingBuilder == null) throw new ArgumentNullException(nameof(loggingBuilder));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var serilog = Utility.CreateConsoleLogger(config);

            return loggingBuilder.AddSerilog(serilog);
        }

        public static ILoggingBuilder AddSerilogConsole(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.Services.AddSingleton<ILoggerProvider, ConsoleExtendedProvider>();
            loggingBuilder.Services.AddSingleton<IConfigureOptions<ConsoleConfiguration>, ConsoleConfigSetup>();
            return loggingBuilder;
        }

        public static ILoggingBuilder AddSerilogConsole(this ILoggingBuilder loggingBuilder, Action<ConsoleConfiguration> configure)
        {
            loggingBuilder.AddSerilogConsole();
            loggingBuilder.Services.Configure(configure);
            return loggingBuilder;
        }
    }
}
