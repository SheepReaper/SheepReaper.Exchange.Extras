using System;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;

namespace SheepReaper.Exchange.PowerShell
{
    [Cmdlet(VerbsDiagnostic.Test, "ExchangeConnection")]
    public class TestExchangeConnectionCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.UseDefaultCredentials = true;

            service.TraceEnabled = true;
            service.TraceEnablePrettyPrinting = true;
            service.TraceFlags = TraceFlags.All;
            service.TraceListener = new TraceListener(this);

            service.AutodiscoverUrl("user@example.com", RedirectionValidationCallback);
        }

        private static bool RedirectionValidationCallback(string redirectionUrl)
        {
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }

            Console.WriteLine("Connection Successful");

            return result;
        }

        class TraceListener : ITraceListener
        {
            private Cmdlet _cmdletInstance;

            public TraceListener(Cmdlet cmdletInstance)
            {
                _cmdletInstance = cmdletInstance;
            }

            public void Trace(string traceType, string traceMessage)
            {
                this._cmdletInstance.WriteVerbose($"[EWS-{traceType}] {traceMessage}");
            }
        }
    }
}
