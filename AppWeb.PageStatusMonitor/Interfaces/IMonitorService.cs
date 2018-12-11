using System;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResultItem Check(MonitorItem monitorItem);
        MonitorResult RunChecks(MonitorConfiguration monitorConfiguration);
    }
}
