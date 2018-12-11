using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Enums;
using AppWeb.PageStatusMonitor.Interfaces;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Services
{
    public class MonitorService : IMonitorService
    {
        private IHttpService _httpService { get; set; }
               
        public MonitorService()
        {
            _httpService = new HttpService();
        }

        public MonitorService(IHttpService httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
        }

        /// <summary>
        /// Runs check on single item
        /// </summary>
        /// <param name="monitorItem">The monitor item to check</param>
        /// <returns>Check result</returns>
        public MonitorResultItem Check(MonitorItem monitorItem)
        {
            if (monitorItem == null)
                throw new ArgumentNullException(nameof(monitorItem));

            return _checkItem(monitorItem);
        }

        /// <summary>
        /// Runs checks provided in the monitor configuration
        /// </summary>
        /// <param name="monitorConfiguration">The monitor configuration to check</param>
        /// <returns>The check results</returns>
        public MonitorResult RunChecks(MonitorConfiguration monitorConfiguration)
        {
            if (monitorConfiguration == null)
                throw new ArgumentNullException(nameof(monitorConfiguration));

            MonitorResult result = new MonitorResult();

            Parallel.ForEach(
                monitorConfiguration.MonitorItems, 
                new ParallelOptions() { MaxDegreeOfParallelism = monitorConfiguration.MaxDegreeOfParallelism }, 
                monitorItem => {
                    var monitorResultItem = _checkItem(monitorItem);
                    result.Results.Add(monitorResultItem);
                    monitorConfiguration.OnCheckCompleteAction?.Invoke(monitorResultItem);
                }
            );

            return result;
        }

        private MonitorResultItem _checkItem(MonitorItem monitorItem)
        {
            if (monitorItem == null)
                throw new ArgumentNullException(nameof(monitorItem));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool successfull = false;

            switch (monitorItem.CheckType)
            {
                case CheckType.HttpGet:
                    if (monitorItem.Uri != null)
                    {
                        successfull = _httpService.GetIsSuccessfull(monitorItem.Uri);
                    }
                    else if (monitorItem.IPAddress != null)
                    {
                        successfull = _httpService.GetIsSuccessfull(new Uri($"http://{monitorItem.IPAddress}"));
                    }
                    break;
                case CheckType.Ping:
                    if (monitorItem.Uri != null)
                    {
                        successfull = _httpService.PingIsSuccessfull(monitorItem.Uri);
                    }
                    else if (monitorItem.IPAddress != null)
                    {
                        successfull = _httpService.PingIsSuccessfull(monitorItem.IPAddress);
                    }
                    break;
            }

            stopwatch.Stop();

            return new MonitorResultItem(monitorItem, successfull, stopwatch.ElapsedMilliseconds);
        }

        public void Dispose()
        {
            _httpService.Dispose();
        }
    }
}
