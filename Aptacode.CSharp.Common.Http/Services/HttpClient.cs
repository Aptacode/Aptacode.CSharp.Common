using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Services.Responses;

namespace Aptacode.CSharp.Common.Http.Services
{
    public interface IHttpClient
    {
        Task<HttpServiceResponse<TReturn>> Send<TReturn>(HttpRequestMessage requestMessage);
    }

    public class DefaultHttpClient : IHttpClient
    {
        public static HttpClient Client = new HttpClient();

        public async Task<HttpServiceResponse<TReturn>> Send<TReturn>(HttpRequestMessage requestMessage)
        {
            var response = await Client.SendAsync(requestMessage).ConfigureAwait(false);
            return await HttpServiceResponse<TReturn>.Create(response).ConfigureAwait(false);
        }
    }

    public class CookieHttpClient : IHttpClient
    {
        public static HttpClient Client = new HttpClient(new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = new CookieContainer()
        });

        public async Task<HttpServiceResponse<TReturn>> Send<TReturn>(HttpRequestMessage requestMessage)
        {
            var response = await Client.SendAsync(requestMessage).ConfigureAwait(false);
            return await HttpServiceResponse<TReturn>.Create(response).ConfigureAwait(false);
        }
    }
}