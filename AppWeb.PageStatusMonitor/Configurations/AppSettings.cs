using AppWeb.PageStatusMonitor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeb.PageStatusMonitor.Configurations
{
    public class AppSettings
    {
        public List<MonitorItem> MonitorItems { get; set; }
        public EmailConfiguration EmailConfiguration { get; set; }

        public AppSettings()
        {
        }
    }
}
