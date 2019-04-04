using System.Collections.Generic;
using MEL = Microsoft.Extensions.Logging;

namespace Serilog.Microsoft.Logging.Console
{
    /// <summary>
    /// Configuration object for console with serilog
    /// </summary>
    public class ConsoleConfiguration
    {
        /// <summary>
        /// Log Levels for Console Logging with Serilog
        /// </summary>
        public Dictionary<string, MEL.LogLevel> LogLevel { get; set; } = new Dictionary<string, MEL.LogLevel> { ["Default"] = MEL.LogLevel.Information };

        /// <summary>
        /// Serilog Console Theme
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Include scopes in console loggin
        /// </summary>
        public bool IncludeScopes { get; set; } = false;

        /// <summary>
        /// The template of the console log
        /// </summary>
        public string Template { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// The async buffer size
        /// </summary>
        public int? AsyncBufferSize { get; set; } = 10000;

        /// <summary>
        /// Drop logs when async buffer reaches max
        /// </summary>
        public bool DropLogsOnBufferLimit { get; set; } = false;
    }
}
