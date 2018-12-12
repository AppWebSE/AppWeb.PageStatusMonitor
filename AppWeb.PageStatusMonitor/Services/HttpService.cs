using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Interfaces;

namespace AppWeb.PageStatusMonitor.Services
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

        public async Task<bool> GetIsSuccessfullAsync(Uri uri)
        {
            if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			try
            {
                var response = await _httpClient.GetAsync(uri);
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

        public async Task<bool> PingIsSuccessfullAsync(Uri uri)
        {
            if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri));
			}

			var pinger = new Ping();
            PingReply reply = await pinger.SendPingAsync(uri.Host);

            return reply.Status == IPStatus.Success;
        }

        public async Task<bool> PingIsSuccessfullAsync(IPAddress ipAddress)
        {
            if (ipAddress == null)
			{
				throw new ArgumentNullException(nameof(ipAddress));
			}

			var pinger = new Ping();
            PingReply reply = await pinger.SendPingAsync(ipAddress);

            return reply.Status == IPStatus.Success;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
