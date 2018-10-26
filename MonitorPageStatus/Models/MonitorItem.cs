using MonitorPageStatus.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MonitorPageStatus.Models
{
    public class MonitorItem
    {
        public Uri Uri { get; set; }
        public IPAddress IPAddress { get; set; }
        public MonitorTypeEnum Type { get; set; }

        public MonitorItem(Uri uri, MonitorTypeEnum monitorType = MonitorTypeEnum.HttpGet)
        {
            Uri = uri;
            Type = monitorType;
        }
        
        public MonitorItem(IPAddress ipAddress, MonitorTypeEnum monitorType = MonitorTypeEnum.HttpGet)
        {
            IPAddress = ipAddress;
            Type = monitorType;
        }

        public override string ToString()
        {
            var stringResult = string.Empty;

            if(Uri != null)
            {
                stringResult += Uri.ToString();
            }

            if(IPAddress != null)
            {
                stringResult += IPAddress.ToString();
            }

            return stringResult;
        }
    }
}
