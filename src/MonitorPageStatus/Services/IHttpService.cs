using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorPageStatus.Services
{
    public interface IHttpService : IDisposable
    {
        bool CanReachUrl(Uri uri);
    }
}
