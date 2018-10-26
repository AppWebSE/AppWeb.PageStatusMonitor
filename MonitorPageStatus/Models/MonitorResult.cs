using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Models
{
    public class MonitorResult
    {
        public MonitorItem MonitorItem { get; set; }
        public bool Success { get; set; }
        public long Milliseconds { get; set; }
        
        public MonitorResult(MonitorItem monitorItem, bool success, long milliSeconds)
        {
            MonitorItem = monitorItem;
            Success = success;
            Milliseconds = milliSeconds;
        }
    }
}
