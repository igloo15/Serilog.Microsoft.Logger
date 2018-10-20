using MEL = Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.Configuration
{
    public class FileConfiguration
    {
        public Dictionary<string, MEL.LogLevel> LogLevel { get; set; } = new Dictionary<string, MEL.LogLevel> { ["Default"] = MEL.LogLevel.Information };

        public bool Shared { get; set; } = false;

        public string Template { get; set; } = "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}";

        public int FlushInterval { get; set; } = 2;

        public string PathFormat { get; set; } = "App.log";

        public int FileCountLimit { get; set; } = 31;

        public long FileSizeLimit { get; set; } = 1073741824;

        public bool IncludeScopes { get; set; } = false;

        public int? BufferSize { get; set; }

        public bool IsJson { get; set; } = false;
    }
}
