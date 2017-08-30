using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Services
{
    public interface IHttpService
    {
        bool CanReachUrl(string url);
    }
}
