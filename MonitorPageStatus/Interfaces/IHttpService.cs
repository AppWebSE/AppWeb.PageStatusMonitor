using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPageStatus.Interfaces
{
    public interface IHttpService : IDisposable
    {
        bool IsReachable(Uri uri);
    }
}
