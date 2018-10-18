using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Microsoft.Logger.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serilog.Microsoft.Logger.Core
{
    internal static class Utility
    {
        
        public static LogEventLevel ConvertLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.None:
                case LogLevel.Critical:
                    return LogEventLevel.Fatal;
                case LogLevel.Error:
                    return LogEventLevel.Error;
                case LogLevel.Warning:
                    return LogEventLevel.Warning;
                case LogLevel.Information:
                    return LogEventLevel.Information;
                case LogLevel.Debug:
                    return LogEventLevel.Debug;
                case LogLevel.Trace:
                    return LogEventLevel.Verbose;
                default:
                    return LogEventLevel.Information;
            }

        }

        public static Serilog.Core.Logger CreateFileLogger(FileConfiguration config)
        {
            if (config?.PathFormat == null) throw new ArgumentNullException(nameof(config.PathFormat));

            var formatter = new MessageTemplateTextFormatter(config.Template, null);
            var bufferSize = config.BufferSize.HasValue ? config.BufferSize.Value : 10000;
            var configuration = new LoggerConfiguration()
                .MinimumLevel.Is(GetMinimumLogLevel(config.LogLevel))
                .Enrich.FromLogContext()
                .WriteTo.Async(w => w.File(
                    formatter,
                    Environment.ExpandEnvironmentVariables(config.PathFormat),
                    fileSizeLimitBytes: config.FileSizeLimit,
                    retainedFileCountLimit: config.FileCountLimit,
                    shared: config.Shared,
                    flushToDiskInterval: TimeSpan.FromSeconds(config.FlushInterval),
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true), bufferSize: bufferSize, blockWhenFull: !config.BufferSize.HasValue);
            

            configuration.Enrich.With<EventNumEnricher>();

            if (config.IncludeScopes)
                configuration.Enrich.With<ScopeEnricher>();

            foreach (var levelOverride in config.LogLevel ?? new Dictionary<string, LogLevel>())
            {
                configuration.MinimumLevel.Override(levelOverride.Key, ConvertLogLevel(levelOverride.Value));
            }

            return configuration.CreateLogger();
        }


        public static LogEventLevel GetMinimumLogLevel(Dictionary<string, LogLevel> levels)
        {
            var minimumLevel = LogEventLevel.Information;

            if (levels.TryGetValue("Default", out LogLevel level))
                return ConvertLogLevel(level);

            return minimumLevel;
        }


    }
}
