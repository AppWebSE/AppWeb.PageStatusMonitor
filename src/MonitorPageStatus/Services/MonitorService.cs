using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPageStatus.Services
{
    public class MonitorService : IMonitorService
    {
        IEmailService _emailService;
        IHttpService _httpService;
        MonitorConfiguration _monitorConfiguration;

        public MonitorService(MonitorConfiguration monitorConfiguration, EmailConfiguration emailConfiguration = null, HttpConfiguration httpConfiguration = null)
        {
            if(emailConfiguration != null)
            {
                _emailService = new EmailService(emailConfiguration);
            }

            if(httpConfiguration == null)
            {
                httpConfiguration = new HttpConfiguration();
            }

            _httpService = new HttpService(httpConfiguration);
            _monitorConfiguration = monitorConfiguration;
        }
        
        public List<MonitorResult> Monitor()
        {
            List<MonitorResult> monitorResults = new List<MonitorResult>();

            List<Task> tasks = new List<Task>();
            foreach (var monitorUri in _monitorConfiguration.MonitorUris)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    bool success = false;
                    switch (monitorUri.Type)
                    {
                        case MonitorTypeEnum.HttpGet:
                            success = _httpService.CanReachUrl(monitorUri.Uri);
                            break;
                    }
                    monitorResults.Add(new MonitorResult(monitorUri.Uri, success));
                });
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            if (_emailService != null 
                && _monitorConfiguration.SendEmailWhenDown 
                && monitorResults.Any(x => !x.Success))
            {
                // todo: send email
                // report list of uri's down
                //emailService.SendEmail(to, from, subject, body, true);
            }

            return monitorResults;
        }

        public void Dispose()
        {
            _httpService.Dispose();
            _emailService.Dispose();
        }
    }
}
