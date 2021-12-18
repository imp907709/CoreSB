

namespace CoreSB.Universal.Infrastructure.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class NetHttpClient : IHttpClient, IDisposable
    {
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _factory;
        private readonly ISerialization _serializer;
        public NetHttpClient(IHttpClientFactory factory, ISerialization serializer)
        {
            _client = _factory.CreateClient();
            _serializer = serializer;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string body)
        {
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            return await _client.PostAsync(url, content);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
