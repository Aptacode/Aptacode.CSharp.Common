using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Services.Interfaces;
using Newtonsoft.Json;

namespace Aptacode.CSharp.Common.Http.Services
{
    public abstract class GenericHttpApiServiceClient
    {
        protected static HttpClient HttpClient = new HttpClient();
        protected readonly HttpRouteBuilder ApiRouteBuilder;
        protected IAccessTokenService AuthService;

        protected GenericHttpApiServiceClient(IAccessTokenService authService, ServerAddress serverAddress,
            params object[] routeSegments)
        {
            ApiRouteBuilder = new HttpRouteBuilder(serverAddress, routeSegments);
            AuthService = authService;
        }

        protected async Task<IEnumerable<TViewModel>> GetAll<TViewModel>(params object[] routeSegments)
        {
            var response =
                await HttpClient
                    .SendAsync(GetRequestTemplate(HttpMethod.Get, ApiRouteBuilder.BuildRoute(routeSegments)))
                    .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<TViewModel>>(body);
        }

        protected async Task<TViewModel> Get<TViewModel>(params object[] routeSegments)
        {
            var response =
                await HttpClient
                    .SendAsync(GetRequestTemplate(HttpMethod.Get, ApiRouteBuilder.BuildRoute(routeSegments)))
                    .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TViewModel>(body);
        }

        protected async Task<TGetViewModel> Put<TGetViewModel, TPutViewModel>(TPutViewModel entity,
            params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Put, ApiRouteBuilder.BuildRoute(routeSegments));
            req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());
            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TGetViewModel>(body);
        }

        protected async Task<TGetViewModel> Post<TGetViewModel, TPutViewModel>(TPutViewModel entity,
            params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Post, ApiRouteBuilder.BuildRoute(routeSegments));
            req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());
            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TGetViewModel>(body);
        }

        protected async Task<bool> Delete(params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Delete, ApiRouteBuilder.BuildRoute(routeSegments));
            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        protected HttpRequestMessage GetRequestTemplate(HttpMethod method, string endpoint)
        {
            var accessToken = AuthService.GetAccessToken();
            if (accessToken == null)
            {
                throw new Exception("You are not authorized to view this content");
            }

            var req = new HttpRequestMessage {Method = method, RequestUri = new Uri(endpoint)};
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return req;
        }
    }
}