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

        }

        public HttpConfiguration(int timeoutSeconds)
        {
            Timeout = new TimeSpan(ticks: timeoutSeconds * 1000);
        }

    }
}
