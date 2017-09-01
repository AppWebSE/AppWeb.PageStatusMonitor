using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Configurations
{
    public class HttpConfiguration
    {
        public TimeSpan Timeout { get; set; }

        public HttpConfiguration()
        {
            Timeout = new TimeSpan(ticks: 15 * 1000);
        }

        public HttpConfiguration(int timeoutSeconds)
        {
            Timeout = new TimeSpan(ticks: timeoutSeconds * 1000);
        }

    }
}
