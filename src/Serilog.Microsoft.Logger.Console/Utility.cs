using Microsoft.Extensions.Logging;
using Serilog.Configuration;
using Serilog.Microsoft.Logger.Core;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;

namespace Serilog.Microsoft.Logging.Console
{
    internal static class Utility
    {
        public static Serilog.Core.Logger CreateConsoleLogger(ConsoleConfiguration config)
        {
            var bufferSize = config.AsyncBufferSize.HasValue ? config.AsyncBufferSize.Value : 10000;
            var configuration = new LoggerConfiguration()
                .MinimumLevel.Is(CoreUtility.GetMinimumLogLevel(config.LogLevel))
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
                configuration.MinimumLevel.Override(levelOverride.Key, CoreUtility.ConvertLogLevel(levelOverride.Value));
            }

            return configuration.CreateLogger();
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
