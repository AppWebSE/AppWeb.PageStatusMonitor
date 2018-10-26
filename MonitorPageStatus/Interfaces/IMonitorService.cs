using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPageStatus.Interfaces
{
    public interface IMonitorService : IDisposable
    {
        List<MonitorResult> Monitor();
    }
}
