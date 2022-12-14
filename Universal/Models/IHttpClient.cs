using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreSB.Universal
{

    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string url, string body);
    }
}
