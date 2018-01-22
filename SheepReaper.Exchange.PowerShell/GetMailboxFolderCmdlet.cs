using System;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;

namespace SheepReaper.Exchange.PowerShell
{
    [Cmdlet(VerbsCommon.Get,"MailboxFolder")]
    public class GetMailboxFolderCmdlet : Cmdlet
    {
        static void Herp()
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.Credentials = new WebCredentials("user@example.com", "Password");
            service.UseDefaultCredentials = false;
            service.AutodiscoverUrl("user@example.com", RediractionValidationCallback);
        }

        private static bool RediractionValidationCallback(string redirectionUrl)
        {
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }
    }
}
