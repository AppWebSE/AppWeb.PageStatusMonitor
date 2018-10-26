using System;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Models;

namespace MonitorPageStatus.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResult RunChecks(MonitorConfiguration configuration);
    }
}
