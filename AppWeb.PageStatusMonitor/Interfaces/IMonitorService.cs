using System;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResult Check(MonitorItem monitorItem);
        MonitorResult RunChecks(MonitorConfiguration monitorConfiguration);
    }
}
