using MEL = Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.Configuration
{
    /// <summary>
    /// The Configuration object for File logging with Serilog
    /// </summary>
    public class FileConfiguration
    {
        private const int ByteMegaByteConversion = 1000 * 1000;

        /// <summary>
        /// The log levels for different loggers
        /// </summary>
        public Dictionary<string, MEL.LogLevel> LogLevel { get; set; } = new Dictionary<string, MEL.LogLevel> { ["Default"] = MEL.LogLevel.Information };

        /// <summary>
        /// Share the log file with other processes. Ignored if using render json
        /// </summary>
        public bool Shared { get; set; } = false;

        /// <summary>
        /// The template of log messages in file. Ignored if using render json
        /// </summary>
        public string Template { get; set; } = "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}";

        /// <summary>
        /// The interval at which the logs are flushed to file in seconds
        /// </summary>
        public int FlushInterval { get; set; } = 2;

        /// <summary>
        /// The path to the log file. Can use environment variables in path
        /// </summary>
        public string PathFormat { get; set; } = "App.log";

        /// <summary>
        /// The number of files allowed before old is deleted
        /// </summary>
        public int FileCountLimit { get; set; } = 31;

        /// <summary>
        /// Max size of file before a new file is created in megabytes
        /// </summary>
        public double FileSizeLimitMegaBytes {

            get => FileSizeLimit / ByteMegaByteConversion;

            set
            {
                var tempValue = value;
                if (tempValue > (long.MaxValue / ByteMegaByteConversion))
                    tempValue = (long.MaxValue / ByteMegaByteConversion);

                FileSizeLimit = (long)(tempValue * ByteMegaByteConversion);
            }
        }

        /// <summary>
        /// Max size of file before a new file is created in bytes
        /// </summary>
        public long FileSizeLimit { get; set; } = 1073741824;

        /// <summary>
        /// Include scopes in file logging
        /// </summary>
        public bool IncludeScopes { get; set; } = false;

        /// <summary>
        /// Render log messages as json
        /// </summary>
        public bool RenderJson { get; set; } = false;

        /// <summary>
        /// The interval of time that files should be rolling
        /// </summary>
        public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

        /// <summary>
        /// Buffer flushing to log file
        /// </summary>
        public bool GroupLogging { get; set; } = false;

        /// <summary>
        /// The size of the async buffer if set to null this will not use async
        /// </summary>
        public int? AsyncBufferSize { get; set; } = 10000;

        /// <summary>
        /// Drop logs messages beyond async buffer
        /// </summary>
        public bool DropLogsOnBufferLimit { get; set; } = false;

        
    }
}
