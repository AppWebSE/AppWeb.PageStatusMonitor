﻿using System;
using System.Threading.Tasks;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResultItem Check(MonitorItem monitorItem);
        Task<MonitorResultItem> CheckAsync(MonitorItem monitorItem);
        MonitorResult RunChecks(MonitorConfiguration monitorConfiguration);
		Task<MonitorResult> RunChecksAsync(MonitorConfiguration monitorConfiguration);
	}
}
