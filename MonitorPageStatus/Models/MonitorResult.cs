using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Models
{
    public class MonitorResult
    {
        public List<MonitorResultItem> Results { get; set; }

        public MonitorResult()
        {
            Results = new List<MonitorResultItem>();
        }

        public MonitorResult Then(Action<MonitorResult> action)
        {
            // Run action
            action(this);
            
            return this;
        }

    }
}
