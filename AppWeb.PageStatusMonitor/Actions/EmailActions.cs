using System;
using AppWeb.PageStatusMonitor.Interfaces;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Actions
{
    public class EmailActions
    {
        public static Action<MonitorResultItem, IEmailService> SendEmailOnFail = (monitorResultItem, emailService) =>
        {
            if (!monitorResultItem.Successful)
            {
                string subject = $"Check for '{monitorResultItem.MonitorItem}' failed";
                string body = $"<html><body><h1>Checked failed</h1><p>Check: {monitorResultItem.MonitorItem}</p><p>Successful: {monitorResultItem.Successful}</p><p>Execution time: {monitorResultItem.Milliseconds}</p></body></html>";
                
                emailService.SendEmailAsync(subject, body, true).GetAwaiter().GetResult();
            }
        };
    }
}
