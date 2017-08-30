using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Services
{
    public interface IEmailService : IDisposable
    {
        bool SendEmail(string to, string from, string subject, string body, bool isBodyHtml);
    }
}
