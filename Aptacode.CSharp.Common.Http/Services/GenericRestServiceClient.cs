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

        protected async Task<ServiceResponse<IEnumerable<TViewModel>>> GetAll<TViewModel>(params object[] routeSegments)
        {
            var response =
                await HttpClient
                    .SendAsync(GetRequestTemplate(HttpMethod.Get, ApiRouteBuilder.BuildRoute(routeSegments)))
                    .ConfigureAwait(false);

            return await ServiceResponse<IEnumerable<TViewModel>>.Create(response).ConfigureAwait(false);
        }

        protected async Task<ServiceResponse<TViewModel>> Get<TViewModel>(params object[] routeSegments)
        {
            var response =
                await HttpClient
                    .SendAsync(GetRequestTemplate(HttpMethod.Get, ApiRouteBuilder.BuildRoute(routeSegments)))
                    .ConfigureAwait(false);

            return await ServiceResponse<TViewModel>.Create(response).ConfigureAwait(false);
        }

        protected async Task<ServiceResponse<TGetViewModel>> Put<TGetViewModel, TPutViewModel>(TPutViewModel entity,
            params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Put, ApiRouteBuilder.BuildRoute(routeSegments));
            req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());
            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);

            return await ServiceResponse<TGetViewModel>.Create(response).ConfigureAwait(false);
        }

        protected async Task<ServiceResponse<TGetViewModel>> Post<TGetViewModel, TPutViewModel>(TPutViewModel entity,
            params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Post, ApiRouteBuilder.BuildRoute(routeSegments));
            req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());

            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);

            return await ServiceResponse<TGetViewModel>.Create(response).ConfigureAwait(false);
        }

        protected async Task<ServiceResponse<bool>> Delete(params object[] routeSegments)
        {
            var req = GetRequestTemplate(HttpMethod.Delete, ApiRouteBuilder.BuildRoute(routeSegments));
            var response = await HttpClient.SendAsync(req).ConfigureAwait(false);

            return await ServiceResponse<bool>.Create(response).ConfigureAwait(false);
        }

        protected HttpRequestMessage GetRequestTemplate(HttpMethod method, string endpoint)
        {
            if (!AuthService.HasValidAccessToken)
            {
                return null;
            }

            var req = new HttpRequestMessage {Method = method, RequestUri = new Uri(endpoint)};
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AuthService.AccessToken);
            return req;
        }
    }
}