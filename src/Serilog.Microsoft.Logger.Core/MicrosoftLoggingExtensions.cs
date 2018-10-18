using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;
using Serilog.Microsoft.Logger.Core;
using Serilog.Microsoft.Logger.Core.Configuration;
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
            if (string.IsNullOrWhiteSpace(config.PathFormat))
            {
                SelfLog.WriteLine("Unable to add the file logger: no PathFormat was present in the configuration");
                return loggerFactory;
            }

            return loggerFactory.AddFile(config);
        }

        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, FileConfiguration config)
        {
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
    }
}
