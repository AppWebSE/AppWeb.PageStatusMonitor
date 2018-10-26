using System;

namespace MonitorPageStatus.Interfaces
{
    public interface IHttpService : IDisposable
    {
        bool SuccessfulGetResponse(Uri uri);
        bool SuccessfulPing(Uri uri);
    }
}
