using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Serilog.Microsoft.Logging.Console
{
    internal class ConsoleConfigSetup : ConfigureFromConfigurationOptions<ConsoleConfiguration>
    {
        public ConsoleConfigSetup(ILoggerProviderConfiguration<ConsoleExtendedProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
