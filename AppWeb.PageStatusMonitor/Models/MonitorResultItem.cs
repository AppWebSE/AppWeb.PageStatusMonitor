using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeb.PageStatusMonitor.Models
{
    public class MonitorResultItem
    {
        public MonitorItem MonitorItem { get; set; }
        public bool Successful { get; set; }
        public long Milliseconds { get; set; }

        public MonitorResultItem(MonitorItem monitorItem, bool successful, long milliSeconds)
        {
            if (monitorItem == null)
                throw new ArgumentNullException(nameof(monitorItem));

            MonitorItem = monitorItem;
            Successful = successful;
            Milliseconds = milliSeconds;
        }
    }
}
