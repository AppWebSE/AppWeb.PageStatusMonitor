using System;
using System.Net;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IHttpService : IDisposable
    {
        bool GetIsSuccessfull(Uri uri);
        bool PingIsSuccessfull(Uri uri);
        bool PingIsSuccessfull(IPAddress ipAdress);
    }
}
