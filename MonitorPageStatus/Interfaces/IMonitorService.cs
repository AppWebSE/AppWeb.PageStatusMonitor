using System;
using MonitorPageStatus.Models;

namespace MonitorPageStatus.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        MonitorResult RunChecks();
    }
}
