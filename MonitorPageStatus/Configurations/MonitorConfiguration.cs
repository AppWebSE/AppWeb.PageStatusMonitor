using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Configurations
{
    public class MonitorConfiguration
    {
        public bool SendEmailWhenDown { get; set; }
        public List<MonitorItem> MonitorItems { get; set; }

        public MonitorConfiguration()
        {
            MonitorItems = new List<MonitorItem>();
        }
    }
}
