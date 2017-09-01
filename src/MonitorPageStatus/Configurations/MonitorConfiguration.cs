using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Configurations
{
    public class MonitorConfiguration
    {
        public bool SendEmailWhenDown { get; set; }
        public List<MonitorUri> MonitorUris { get; set; }

        public MonitorConfiguration()
        {
            MonitorUris = new List<MonitorUri>();
        }
    }
}
