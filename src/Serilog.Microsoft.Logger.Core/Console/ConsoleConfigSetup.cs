using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using Serilog.Microsoft.Logger.Core.Configuration;
using Serilog.Microsoft.Logger.Core.File;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core.Console
{
    internal class ConsoleConfigSetup : ConfigureFromConfigurationOptions<ConsoleConfiguration>
    {
        public ConsoleConfigSetup(ILoggerProviderConfiguration<ConsoleExtendedProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
