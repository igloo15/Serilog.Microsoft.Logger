using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Extensions.Logging;
using Serilog.Microsoft.Logger.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.File
{
    /// <summary>
    /// Extended Logger with Options
    /// </summary>
    [ProviderAlias("File")]
    public class FileExtendedProvider : SerilogLoggerProvider
    {
        /// <summary>
        /// Construct a <see cref="SerilogLoggerProvider" />.
        /// </summary>
        /// <param name="options">The Options for this Provider</param>
        public FileExtendedProvider(IOptions<FileConfiguration> options) : base(Utility.CreateFileLogger(options.Value), true)
        {
        }
    }
}
