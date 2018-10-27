using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;
using System.Threading.Tasks;
using System;

namespace MonitorPageStatus.Services
{
    public class MonitorService : IMonitorService
    {
        IEmailService _emailService;
        IHttpService _httpService;

        public MonitorService(EmailConfiguration emailConfiguration = null, HttpConfiguration httpConfiguration = null)
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
        }
        
        public MonitorResult RunChecks(MonitorConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            MonitorResult result = new MonitorResult();

            Parallel.ForEach(configuration.MonitorItems, item =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                bool successfull = false;

                switch (item.CheckType)
                {
                    case CheckType.HttpGet:
                        if(item.Uri != null)
                        {
                            successfull = _httpService.SuccessfulGetResponse(item.Uri);
                        }
                        else if(item.IPAddress != null)
                        {
                            successfull = _httpService.SuccessfulGetResponse(new Uri($"http://{item.IPAddress}"));
                        }
                        break;
                    case CheckType.Ping:
                        if (item.Uri != null)
                        {
                            successfull = _httpService.SuccessfullPing(item.Uri);
                        }
                        else if (item.IPAddress != null)
                        {
                            successfull = _httpService.SuccessfullPing(item.IPAddress);
                        }
                        break;
                }
                
                stopwatch.Stop();
                
                result.Results.Add(new MonitorResultItem(item, successfull, stopwatch.ElapsedMilliseconds));
            });
            
            //todo: Create a chainable method on MonitorResult?
            //if (_emailService != null 
            //    && _monitorConfiguration.SendEmailWhenDown 
            //    && monitorResult.Any(x => !x.Success))
            //{
            //    // todo: send email
            //    // report list of uri's down
            //    // emailService.SendEmail(to, from, subject, body, true);
            //}

            return result;
        }

        public void Dispose()
        {
            _httpService.Dispose();
            _emailService.Dispose();
        }
    }
}
