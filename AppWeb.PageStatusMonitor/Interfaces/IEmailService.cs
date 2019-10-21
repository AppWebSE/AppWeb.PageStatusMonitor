using AppWeb.PageStatusMonitor.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IEmailService : IDisposable
    {
        Task<bool> SendEmailAsync(string subject, string body, bool isBodyHtml);
    }
}
