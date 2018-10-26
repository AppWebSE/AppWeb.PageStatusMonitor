using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Interfaces;

namespace MonitorPageStatus.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpConfiguration httpConfiguration = null)
        {
            _httpClient = new HttpClient();

            if(httpConfiguration == null)
            {
                httpConfiguration = new HttpConfiguration(timeoutSeconds:15);
            }
            
            _httpClient.Timeout = httpConfiguration.Timeout;
        }

        public bool SuccessfulGetResponse(Uri uri)
        {
            try
            {
                var response = _httpClient.GetAsync(uri).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                /* 
                 * todo: 
                 * - handle exception
                 * - check if timeout?
                 * - check statuscode
                 */
            }
            return false;
        }

        public bool SuccessfulPing(Uri uri)
        {
            var pinger = new Ping();
            PingReply reply = pinger.Send(uri.Host);

            return reply.Status == IPStatus.Success;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
