using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Services
{
    public interface IMonitorService
    {
        List<MonitorResult> Monitor();
    }
}
