using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using MEL = Microsoft.Extensions.Logging;

namespace Serilog.Microsoft.Logger.Core.Configuration
{
    public class ConsoleConfiguration
    {
        public Dictionary<string, MEL.LogLevel> LogLevel { get; set; } = new Dictionary<string, MEL.LogLevel> { ["Default"] = MEL.LogLevel.Information };

        public string Theme { get; set; }

        public bool IncludeScopes { get; set; } = false;

        public string Template { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        public int? AsyncBufferSize { get; set; } = 10000;

        public bool DropLogsOnBufferLimit { get; set; } = false;
    }
}
