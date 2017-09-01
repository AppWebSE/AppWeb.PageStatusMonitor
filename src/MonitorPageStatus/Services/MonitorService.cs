using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (emailConfiguration != null)
            {
                _emailService = new EmailService(emailConfiguration);
            }

            if (httpConfiguration == null)
            {
                httpConfiguration = new HttpConfiguration();
            }

            _httpService = new HttpService(httpConfiguration);
            _monitorConfiguration = monitorConfiguration;
        }
        

        public List<MonitorResult> Monitor()
        {
            List<MonitorResult> monitorResults = new List<MonitorResult>();

            Parallel.ForEach(_monitorConfiguration.MonitorUris, monitorUri =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                bool success = _httpService.IsReachable(monitorUri.Uri);

                stopwatch.Stop();
                
                monitorResults.Add(new MonitorResult(monitorUri.Uri, success, stopwatch.ElapsedMilliseconds));
                // todo: is it necesary to lock the section manipulating the list?
                // lock (monitorResults){}

            });

            if (_emailService != null 
                && _monitorConfiguration.SendEmailWhenDown 
                && monitorResults.Any(x => !x.Success))
            {
                // todo: send email
                // report list of uri's down
                // emailService.SendEmail(to, from, subject, body, true);
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
