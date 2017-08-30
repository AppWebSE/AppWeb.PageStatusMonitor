using MonitorPageStatus.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Services
{
    public class MonitorService : IMonitorService, IDisposable
    {
        IEmailService _emailService;
        IHttpService _httpService;

        public MonitorService(MonitorConfiguration monitorConfiguration, EmailConfiguration emailConfiguration, HttpConfiguration httpConfiguration)
        {
            _emailService = new EmailService(emailConfiguration);
            _httpService = new HttpService(httpConfiguration);
        }

        public void Dispose()
        {
            _httpService.Dispose();
            _emailService.Dispose();
        }
    }
}
