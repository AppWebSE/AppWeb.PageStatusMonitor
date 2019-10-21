using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /// Runs check on single item synchronously 
        /// </summary>
        /// <param name="monitorItem">The monitor item to check</param>
        /// <returns>Check result</returns>
        public MonitorResultItem Check(MonitorItem monitorItem)
		{
			if (monitorItem == null)
			{
				throw new ArgumentNullException(nameof(monitorItem));
			}

			return CheckAsync(monitorItem).GetAwaiter().GetResult();
		}

        /// <summary>
        /// Runs check on single item asynchronously 
        /// </summary>
        /// <param name="monitorItem">The monitor item to check</param>
        /// <returns>Check result</returns>
        public async Task<MonitorResultItem> CheckAsync(MonitorItem monitorItem)
		{
			if (monitorItem == null)
			{
				throw new ArgumentNullException(nameof(monitorItem));
			}

            return await RunCheckOnItemAsync(monitorItem, null).ConfigureAwait(false);
		}

        /// <summary>
        /// Runs checks provided in the monitor configuration synchronously 
        /// </summary>
        /// <param name="monitorConfiguration">The monitor configuration to check</param>
        /// <returns>The check results</returns>
        public MonitorResult RunChecks(MonitorConfiguration monitorConfiguration)
		{
			if (monitorConfiguration == null)
			{
				throw new ArgumentNullException(nameof(monitorConfiguration));
			}

			var result = RunChecksAsync(monitorConfiguration).GetAwaiter().GetResult();

			return result;
		}

        /// <summary>
        /// Runs checks provided in the monitor configuration asynchronously 
        /// </summary>
        /// <param name="monitorConfiguration">The monitor configuration to check</param>
        /// <returns>The check results</returns>
        public async Task<MonitorResult> RunChecksAsync(MonitorConfiguration monitorConfiguration)
		{
			if (monitorConfiguration == null)
			{
				throw new ArgumentNullException(nameof(monitorConfiguration));
			}

			var tasks = new List<Task<MonitorResultItem>>();
			foreach (var monitorItem in monitorConfiguration.MonitorItems)
			{
				tasks.Add(RunCheckOnItemAsync(monitorItem, monitorConfiguration));
			}

			var checkResults = await Task.WhenAll(tasks).ConfigureAwait(false);
					   
			return new MonitorResult(){
				Results = checkResults.ToList()
			};
		}		
		 
		private async Task<MonitorResultItem> RunCheckOnItemAsync(MonitorItem monitorItem, MonitorConfiguration monitorConfiguration)
		{
			var checkResult = await CheckItemAsync(monitorItem).ConfigureAwait(false);

			monitorConfiguration?.OnCheckCompleteAction?.Invoke(checkResult);

			return checkResult;
		}

		private async Task<MonitorResultItem> CheckItemAsync(MonitorItem monitorItem)
        {
            if (monitorItem == null)
			{
				throw new ArgumentNullException(nameof(monitorItem));
			}

			Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool successfull = false;

            switch (monitorItem.CheckType)
            {
                case CheckType.HttpGet:
                    if (monitorItem.Uri != null)
                    {
                        successfull = await _httpService.GetIsSuccessfullAsync(monitorItem.Uri).ConfigureAwait(false);
                    }
                    else if (monitorItem.IPAddress != null)
                    {
                        successfull = await _httpService.GetIsSuccessfullAsync(new Uri($"http://{monitorItem.IPAddress}")).ConfigureAwait(false);
                    }
                    break;
                case CheckType.Ping:
                    if (monitorItem.Uri != null)
                    {
                        successfull = await _httpService.PingIsSuccessfullAsync(monitorItem.Uri).ConfigureAwait(false);
                    }
                    else if (monitorItem.IPAddress != null)
                    {
                        successfull = await _httpService.PingIsSuccessfullAsync(monitorItem.IPAddress).ConfigureAwait(false);
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
