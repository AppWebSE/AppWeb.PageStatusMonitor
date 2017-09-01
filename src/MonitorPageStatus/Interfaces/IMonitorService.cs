using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        List<MonitorResult> Monitor();
    }
}
