using MonitorPageStatus.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Models
{
    public class MonitorUri
    {
        public Uri Uri { get; set; }
        public MonitorTypeEnum Type { get; set; }

        public MonitorUri(Uri uri, MonitorTypeEnum monitorTypeEnum = MonitorTypeEnum.HttpGet)
        {
            Uri = uri;
            Type = monitorTypeEnum;
        }

        public MonitorUri(string uri, MonitorTypeEnum monitorTypeEnum = MonitorTypeEnum.HttpGet)
        {
            Uri = new Uri(uri);
            Type = monitorTypeEnum;
        }
    }
}
