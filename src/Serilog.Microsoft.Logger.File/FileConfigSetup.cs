using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Serilog.Microsoft.Logging.File
{
    internal class FileConfigSetup : ConfigureFromConfigurationOptions<FileConfiguration>
    {
        public FileConfigSetup(ILoggerProviderConfiguration<FileExtendedProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
