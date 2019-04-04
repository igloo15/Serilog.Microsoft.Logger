using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Extensions.Logging;

namespace Serilog.Microsoft.Logging.Console
{
    /// <summary>
    /// An Extended Provider for Serilogging to Console
    /// </summary>
    [ProviderAlias("SerilogConsole")]
    internal class ConsoleExtendedProvider : SerilogLoggerProvider
    {
        /// <summary>
        /// Construct a <see cref="SerilogLoggerProvider" />.
        /// </summary>
        /// <param name="options">The Options for this Provider</param>
        public ConsoleExtendedProvider(IOptions<ConsoleConfiguration> options) : base(Utility.CreateConsoleLogger(options.Value), true)
        {
        }
    }
}
