using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Extensions.Logging;

namespace Serilog.Microsoft.Logging.File
{
    /// <summary>
    /// Extended Logger with Options
    /// </summary>
    [ProviderAlias("File")]
    internal class FileExtendedProvider : SerilogLoggerProvider
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
