﻿using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorPageStatus.Services
{
    public class MonitorService : IMonitorService, IDisposable
    {
        IEmailService _emailService;
        IHttpService _httpService;
        MonitorConfiguration _monitorConfiguration;

        public MonitorService(MonitorConfiguration monitorConfiguration, EmailConfiguration emailConfiguration, HttpConfiguration httpConfiguration)
        {
            _emailService = new EmailService(emailConfiguration);
            _httpService = new HttpService(httpConfiguration);
            _monitorConfiguration = monitorConfiguration;
        }
        
        public List<MonitorResult> Monitor()
        {
            List<MonitorResult> results = new List<MonitorResult>();

            foreach (var monitorUri in _monitorConfiguration.MonitorUris)
            {
                bool success = false;
                switch (monitorUri.Type)
                {
                    case MonitorTypeEnum.HttpGet:
                        success = _httpService.CanReachUrl(monitorUri.Uri);
                        break;
                }
                results.Add(new MonitorResult(monitorUri.Uri, success));
            }

            if (_monitorConfiguration.SendEmailWhenDown && results.Any(x => !x.Success))
            {
                // todo: send email
                // report list of uri's down
                //emailService.SendEmail(to, from, subject, body, true);
            }

            return results;
        }

        public void Dispose()
        {
            _httpService.Dispose();
            _emailService.Dispose();
        }
    }
}