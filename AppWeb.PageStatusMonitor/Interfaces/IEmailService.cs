using AppWeb.PageStatusMonitor.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IEmailService : IDisposable
    {
        bool SendEmail(string subject, string body, bool isBodyHtml);
    }
}
