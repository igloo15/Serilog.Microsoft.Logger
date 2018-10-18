using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serilog.Microsoft.Logger.Core
{
    class ScopeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (!logEvent.Properties.ContainsKey("Scope"))
                return;

            var scopes = logEvent.Properties["Scope"] as SequenceValue;

            if (scopes == null)
                return;

            string scopeValues = string.Join(" => ", scopes.Elements.Select(x => x.ToString()));

            logEvent.AddOrUpdateProperty(new LogEventProperty("Scopes", new ScalarValue(scopeValues)));
        }
    }
}
