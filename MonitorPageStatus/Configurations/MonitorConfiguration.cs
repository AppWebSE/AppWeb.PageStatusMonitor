using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Configurations
{
    public class MonitorConfiguration
    {
        public List<MonitorItem> MonitorItems { get; set; }
        public Action<MonitorResultItem> OnCheckCompleteAction { get; set; }
        public int MaxDegreeOfParallelism { get; set; }

        public MonitorConfiguration(List<MonitorItem> monitorItems, Action<MonitorResultItem> onCheckCompleteAction, int? maxDegreeOfParallelism)
        {
            if (monitorItems == null)
                throw new ArgumentNullException(nameof(monitorItems));
            
            MonitorItems = monitorItems;
            OnCheckCompleteAction = onCheckCompleteAction;
            MaxDegreeOfParallelism = maxDegreeOfParallelism ?? 3;
        }
    }
}
