using System;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;

namespace MonitorPageStatus.Actions
{
    public class EmailActions
    {
        public static Action<MonitorResultItem, IEmailService> SendEmailOnFail = (monitorResultItem, emailService) =>
        {
            if (!monitorResultItem.Successful)
            {
                string subject = $"Check for '{monitorResultItem.MonitorItem}' failed";
                string body = $"<html><body><h1>Checked failed</h1><p>Check: {monitorResultItem.MonitorItem}</p><p>Successful: {monitorResultItem.Successful}</p><p>Execution time: {monitorResultItem.Milliseconds}</p></body></html>";
                
                emailService.SendEmail(subject, body, true);
            }
        };
    }
}
