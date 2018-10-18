using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Microsoft.Logger.Core
{
    class EventNumEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("EventId", new ScalarValue(EventIdHash.Compute(logEvent.MessageTemplate.Text))));
        }
    }
}
