using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;

namespace MonitorPageStatus.Services
{
    public class MonitorService : IMonitorService
    {
        MonitorConfiguration _monitorConfiguration;
        IEmailService _emailService;
        IHttpService _httpService;

        public MonitorService(MonitorConfiguration monitorConfiguration, EmailConfiguration emailConfiguration = null, HttpConfiguration httpConfiguration = null)
        {
            if (monitorConfiguration == null)
                throw new ArgumentNullException(nameof(monitorConfiguration));

            _monitorConfiguration = monitorConfiguration;

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
        
        public MonitorResult RunChecks()
        {
            MonitorResult result = new MonitorResult();

            Parallel.ForEach(_monitorConfiguration.MonitorItems, new ParallelOptions() { MaxDegreeOfParallelism = _monitorConfiguration.MaxDegreeOfParallelism }, item =>
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

                var monitorResultItem = new MonitorResultItem(item, successfull, stopwatch.ElapsedMilliseconds);

                _monitorConfiguration.OnCheckCompleteAction?.Invoke(monitorResultItem);

                result.Results.Add(monitorResultItem);
            });
            
            return result;
        }

        public void Dispose()
        {
            _httpService.Dispose();
            _emailService.Dispose();
        }
    }
}
