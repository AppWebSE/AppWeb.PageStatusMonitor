using System;
using System.Net;
using System.Threading.Tasks;

namespace AppWeb.PageStatusMonitor.Interfaces
{
    public interface IHttpService : IDisposable
    {
        Task<bool> GetIsSuccessfullAsync(Uri uri);
		Task<bool> PingIsSuccessfullAsync(Uri uri);
		Task<bool> PingIsSuccessfullAsync(IPAddress ipAdress);
    }
}
