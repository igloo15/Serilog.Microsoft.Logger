using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Debugging;
using Serilog.Microsoft.Logger.Core;
using Serilog.Microsoft.Logger.Core.Configuration;
using Serilog.Microsoft.Logger.Core.Console;
using Serilog.Microsoft.Logger.Core.File;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class MicrosoftLoggingExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, IConfigurationSection configuration)
        {
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = configuration.Get<FileConfiguration>();
            

            return loggerFactory.AddFile(config);
        }

        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, FileConfiguration config)
        {
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (config == null) throw new ArgumentNullException(nameof(config));

            if (string.IsNullOrWhiteSpace(config.PathFormat))
            {
                SelfLog.WriteLine("Unable to add the file logger: no PathFormat was present in the configuration");
                return loggerFactory;
            }

            var serilog = Utility.CreateFileLogger(config);

            return loggerFactory.AddSerilog(serilog, dispose: true);
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            if (loggingBuilder == null) throw new ArgumentNullException(nameof(loggingBuilder));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = configuration.Get<FileConfiguration>();
            if (string.IsNullOrWhiteSpace(config.PathFormat))
            {
                SelfLog.WriteLine("Unable to add the file logger: no PathFormat was present in the configuration");
                return loggingBuilder;
            }

            return loggingBuilder.AddFile(config);
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggingBuilder, FileConfiguration config)
        {
            var serilog = Utility.CreateFileLogger(config);

            return loggingBuilder.AddSerilog(serilog, dispose: true);
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.Services.AddSingleton<ILoggerProvider, FileExtendedProvider>();
            loggingBuilder.Services.AddSingleton<IConfigureOptions<FileConfiguration>, FileConfigSetup>();
            return loggingBuilder;
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggingBuilder, Action<FileConfiguration> configure)
        {
            loggingBuilder.AddFile();
            loggingBuilder.Services.Configure(configure);
            return loggingBuilder;
        }

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
