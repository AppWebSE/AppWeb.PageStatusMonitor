using MonitorPageStatus.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Interfaces
{
    public interface IEmailService : IDisposable
    {
        bool SendEmail(string subject, string body, bool isBodyHtml);
    }
}
