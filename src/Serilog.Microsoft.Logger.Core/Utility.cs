using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Configuration;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Display;
using Serilog.Microsoft.Logger.Core.Configuration;
using Serilog.Sinks.SystemConsole.Themes;
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

            var formatter = config.RenderJson ?
                (ITextFormatter)new RenderedCompactJsonFormatter()
                : new MessageTemplateTextFormatter(config.Template, null);

            var bufferSize = config.AsyncBufferSize.HasValue ? config.AsyncBufferSize.Value : 10000;
            var configuration = new LoggerConfiguration()
                .MinimumLevel.Is(GetMinimumLogLevel(config.LogLevel))
                .Enrich.FromLogContext();

            Action<LoggerSinkConfiguration> configureFile = w => w.File(
                        formatter,
                        Environment.ExpandEnvironmentVariables(config.PathFormat),
                        fileSizeLimitBytes: config.FileSizeLimit,
                        retainedFileCountLimit: config.FileCountLimit,
                        shared: config.Shared,
                        flushToDiskInterval: TimeSpan.FromSeconds(config.FlushInterval),
                        rollingInterval: config.RollingInterval,
                        rollOnFileSizeLimit: true,
                        buffered: config.GroupLogging);

            if (config.AsyncBufferSize.HasValue)
            {
                configuration = configuration
                    .WriteTo.Async(configureFile,
                        bufferSize: bufferSize,
                        blockWhenFull: !config.DropLogsOnBufferLimit);
            }
            else
            {
                configureFile(configuration.WriteTo);
            }
            
            if(!config.RenderJson)
                configuration.Enrich.With<EventNumEnricher>();

            if (!config.RenderJson && config.IncludeScopes)
                configuration.Enrich.With<ScopeEnricher>();

            foreach (var levelOverride in config.LogLevel ?? new Dictionary<string, LogLevel>())
            {
                configuration.MinimumLevel.Override(levelOverride.Key, ConvertLogLevel(levelOverride.Value));
            }

            return configuration.CreateLogger();
        }

        public static Serilog.Core.Logger CreateConsoleLogger(ConsoleConfiguration config)
        {
            var bufferSize = config.AsyncBufferSize.HasValue ? config.AsyncBufferSize.Value : 10000;
            var configuration = new LoggerConfiguration()
                .MinimumLevel.Is(GetMinimumLogLevel(config.LogLevel))
                .Enrich.FromLogContext();

            Action<LoggerSinkConfiguration> configureConsole = w => w.Console(
                        outputTemplate: config.Template,
                        theme: GetTheme(config.Theme));

            if (config.AsyncBufferSize.HasValue)
            {
                configuration
                    .WriteTo.Async(configureConsole,
                        bufferSize: bufferSize,
                        blockWhenFull: !config.AsyncBufferSize.HasValue);
            }
            else
            {
                configureConsole(configuration.WriteTo);
            }
            

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

        public static ConsoleTheme GetTheme(string theme)
        {
            switch (theme)
            {
                case "None":
                    return ConsoleTheme.None;
                case "SystemConsoleThemeLiterate":
                    return SystemConsoleTheme.Literate;
                case "SystemConsoleThemeGrayscale":
                    return SystemConsoleTheme.Grayscale;
                case "SystemConsoleThemeColored":
                    return SystemConsoleTheme.Colored;
                case "SystemConsoleThemeNone":
                    return ConsoleTheme.None;
                case "AnsiConsoleThemeCode":
                    return AnsiConsoleTheme.Code;
                case "AnsiConsoleThemeGrayscale":
                    return AnsiConsoleTheme.Grayscale;
                case "AnsiConsoleThemeLiterate":
                    return AnsiConsoleTheme.Literate;
                case "AnsiConsoleThemeNone":
                    return ConsoleTheme.None;
                default:
                    return SystemConsoleTheme.Literate;
            }
        }

    }
}
