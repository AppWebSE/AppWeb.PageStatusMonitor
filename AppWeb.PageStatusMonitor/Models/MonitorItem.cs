using System;
using System.Net;
using AppWeb.PageStatusMonitor.Enums;

namespace AppWeb.PageStatusMonitor.Models
{
    public class MonitorItem
    {
        public string CheckUri { get; set; }
        private Uri _uri { get; set; }
        public Uri Uri {
            get
            {
                if (_uri != null)
				{
					return _uri;
				}

				if (!string.IsNullOrEmpty(CheckUri)) {
                    _uri = new Uri(CheckUri);
                    return _uri;
                }

                return null;
            }
            set
            {
                _uri = value;
            }
        }

        public string CheckIPAddress { get; set; }
        private IPAddress _ipAddress { get; set; }
        public IPAddress IPAddress {
            get {
                if (_ipAddress != null)
				{
					return _ipAddress;
				}

				if (!string.IsNullOrEmpty(CheckIPAddress))
                {
                    _ipAddress = IPAddress.Parse(CheckIPAddress);
                    return _ipAddress;
                }

                return null;
            }
            set
            {
                _ipAddress = value;
            }
        }

        public CheckType CheckType { get; set; }

        public MonitorItem()
        {
        }

        public MonitorItem(Uri uri, CheckType checkType = CheckType.HttpGet)
        {
			_uri = uri ?? throw new ArgumentNullException(nameof(uri));
            CheckType = checkType;
        }

        public MonitorItem(IPAddress ipAddress, CheckType checkType = CheckType.HttpGet)
        {
			_ipAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
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
