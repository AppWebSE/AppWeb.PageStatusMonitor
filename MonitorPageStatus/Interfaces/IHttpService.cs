using System;
using System.Net;

namespace MonitorPageStatus.Interfaces
{
    public interface IHttpService : IDisposable
    {
        bool SuccessfulGetResponse(Uri uri);
        bool SuccessfullPing(Uri uri);
        bool SuccessfullPing(IPAddress ipAdress);
    }
}
