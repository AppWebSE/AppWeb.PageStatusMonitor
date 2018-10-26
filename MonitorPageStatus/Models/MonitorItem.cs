using System;
using System.Net;
using MonitorPageStatus.Enums;

namespace MonitorPageStatus.Models
{
    public class MonitorItem
    {
        public Uri Uri { get; set; }
        public IPAddress IPAddress { get; set; }
        public CheckType CheckType { get; set; }

        public MonitorItem(Uri uri, CheckType checkType = CheckType.HttpGet)
        {
            Uri = uri;
            CheckType = checkType;
        }
        
        public MonitorItem(IPAddress ipAddress, CheckType checkType = CheckType.HttpGet)
        {
            IPAddress = ipAddress;
            CheckType = checkType;
        }

        public override string ToString()
        {
            var stringResult = $"{CheckType.ToString()}: ";

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
