using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using Serilog.Microsoft.Logger.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.File
{
    internal class FileConfigSetup : ConfigureFromConfigurationOptions<FileConfiguration>
    {
        public FileConfigSetup(ILoggerProviderConfiguration<FileExtendedProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
