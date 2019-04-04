using Microsoft.Extensions.Logging;
using Serilog.Events;
using System.Collections.Generic;

namespace Serilog.Microsoft.Logger.Core
{
    public static class CoreUtility
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

        

        

        public static LogEventLevel GetMinimumLogLevel(Dictionary<string, LogLevel> levels)
        {
            var minimumLevel = LogEventLevel.Information;

            if (levels.TryGetValue("Default", out LogLevel level))
                return ConvertLogLevel(level);

            return minimumLevel;
        }
    }
}
