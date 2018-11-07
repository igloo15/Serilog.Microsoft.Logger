using MEL = Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.Configuration
{
    public class FileConfiguration
    {
        private const int ByteMegaByteConversion = 1000 * 1000;

        public Dictionary<string, MEL.LogLevel> LogLevel { get; set; } = new Dictionary<string, MEL.LogLevel> { ["Default"] = MEL.LogLevel.Information };

        public bool Shared { get; set; } = false;

        public string Template { get; set; } = "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}";

        public int FlushInterval { get; set; } = 2;

        public string PathFormat { get; set; } = "App.log";

        public int FileCountLimit { get; set; } = 31;

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

        public long FileSizeLimit { get; set; } = 1073741824;

        public bool IncludeScopes { get; set; } = false;

        public bool RenderJson { get; set; } = false;

        public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

        public bool GroupLogging { get; set; } = false;

        public int? AsyncBufferSize { get; set; } = 10000;

        public bool DropLogsOnBufferLimit { get; set; } = false;

        
    }
}
