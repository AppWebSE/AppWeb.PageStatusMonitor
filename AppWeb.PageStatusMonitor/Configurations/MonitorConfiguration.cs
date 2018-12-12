using AppWeb.PageStatusMonitor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeb.PageStatusMonitor.Configurations
{
    public class MonitorConfiguration
    {
        public List<MonitorItem> MonitorItems { get; set; }
        public Action<MonitorResultItem> OnCheckCompleteAction { get; set; }

        public MonitorConfiguration(List<MonitorItem> monitorItems, Action<MonitorResultItem> onCheckCompleteAction)
        {            
            MonitorItems = monitorItems ?? throw new ArgumentNullException(nameof(monitorItems));
			OnCheckCompleteAction = onCheckCompleteAction;
        }
    }
}
