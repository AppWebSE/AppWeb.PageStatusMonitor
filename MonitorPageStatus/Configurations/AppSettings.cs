using MonitorPageStatus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Configurations
{
    public class AppSettings
    {
        public int MaxDegreeOfParallelism { get; set; }
        public List<MonitorItem> MonitorItems { get; set; }
        public EmailConfiguration EmailConfiguration { get; set; }

        public AppSettings()
        {
            MaxDegreeOfParallelism = 3;
        }
    }
}
