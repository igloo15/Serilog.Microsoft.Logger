using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.Configuration
{
    internal class FileConfiguration
    {
        public Dictionary<string, LogLevel> LogLevel { get; set; }

        public bool Shared { get; set; } = false;

        public string Template { get; set; } = "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}";

        public int FlushInterval { get; set; } = 5;

        public string PathFormat { get; set; } = "App.log";

        public int FileCountLimit { get; set; } = 31;

        public long FileSizeLimit { get; set; } = 1073741824;

        public bool IncludeScopes { get; set; } = false;

        public int? BufferSize { get; set; }
    }
}
