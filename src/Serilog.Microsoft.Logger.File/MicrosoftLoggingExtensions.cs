using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Debugging;
using Serilog.Microsoft.Logging.File;
using System;

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

        
    }
}
