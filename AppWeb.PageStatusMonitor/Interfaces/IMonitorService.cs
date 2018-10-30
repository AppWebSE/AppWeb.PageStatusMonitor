using System;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResult RunChecks();
    }
}
