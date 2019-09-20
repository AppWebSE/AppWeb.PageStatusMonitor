using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeb.PageStatusMonitor.Configurations
{
    public class HttpConfiguration
    {
        public TimeSpan Timeout { get; set; }

        public HttpConfiguration()
        {
            Timeout = TimeSpan.FromSeconds(15);
        }

        public HttpConfiguration(int timeoutSeconds)
        {
            Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

    }
}
