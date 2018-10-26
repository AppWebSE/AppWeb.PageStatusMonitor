using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Models
{
    public class MonitorResult
    {
        public Uri Uri { get; set; }
        public bool Success { get; set; }
        public long Milliseconds { get; set; }

        public MonitorResult(Uri uri, bool success)
        {
            Uri = uri;
            Success = success;
        }

        public MonitorResult(Uri uri, bool success, long milliSeconds)
        {
            Uri = uri;
            Success = success;
            Milliseconds = milliSeconds;
        }
    }
}
