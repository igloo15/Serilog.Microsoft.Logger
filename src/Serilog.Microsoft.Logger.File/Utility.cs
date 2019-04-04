using Microsoft.Extensions.Logging;
using Serilog.Configuration;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Display;
using Serilog.Microsoft.Logging.Core;
using System;
using System.Collections.Generic;

namespace Serilog.Microsoft.Logging.File
{
    internal static class Utility
    {
        public static Serilog.Core.Logger CreateFileLogger(FileConfiguration config)
        {
            if (config?.PathFormat == null) throw new ArgumentNullException(nameof(config.PathFormat));

            var formatter = config.RenderJson ?
                (ITextFormatter)new RenderedCompactJsonFormatter()
                : new MessageTemplateTextFormatter(config.Template, null);

            var bufferSize = config.AsyncBufferSize.HasValue ? config.AsyncBufferSize.Value : 10000;
            var configuration = new LoggerConfiguration()
                .MinimumLevel.Is(CoreUtility.GetMinimumLogLevel(config.LogLevel))
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

            if (!config.RenderJson)
                configuration.Enrich.With<EventNumEnricher>();

            if (!config.RenderJson && config.IncludeScopes)
                configuration.Enrich.With<ScopeEnricher>();

            foreach (var levelOverride in config.LogLevel ?? new Dictionary<string, LogLevel>())
            {
                configuration.MinimumLevel.Override(levelOverride.Key, CoreUtility.ConvertLogLevel(levelOverride.Value));
            }

            return configuration.CreateLogger();
        }
    }
}
