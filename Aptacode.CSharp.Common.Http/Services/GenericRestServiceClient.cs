using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Services.Interfaces;
using Aptacode.CSharp.Common.Http.Services.Responses;

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

        protected async Task<HttpServiceResponse<TReturn>> GenericHttpMethod<TReturn, TSend>(HttpMethod method,
            TSend content, params object[] routeSegments)
        {
            var requestMessage = CreateRequestMessage(method, content, routeSegments);
            if (requestMessage == null)
            {
                return HttpServiceResponse<TReturn>.Create("Could not create request");
            }

            var response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            return await HttpServiceResponse<TReturn>.Create(response).ConfigureAwait(false);
        }

        protected async Task<HttpServiceResponse<TReturn>> GenericHttpMethod<TReturn>(HttpMethod method,
            params object[] routeSegments)
        {
            var requestMessage = CreateRequestMessage(method, routeSegments);
            if (requestMessage == null)
            {
                return HttpServiceResponse<TReturn>.Create("Could not create request");
            }

            var response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            return await HttpServiceResponse<TReturn>.Create(response).ConfigureAwait(false);
        }

        protected HttpRequestMessage CreateRequestMessage(HttpMethod method, params object[] routeSegments) =>
            new HttpRequestMessage()
                .SetMethod(method)
                .SetRoute(ApiRouteBuilder.BuildRoute(routeSegments))
                .AddAuthentication(AuthService);

        protected HttpRequestMessage CreateRequestMessage<TContent>(HttpMethod method, TContent content,
            params object[] routeSegments) =>
            new HttpRequestMessage()
                .SetMethod(method)
                .SetRoute(ApiRouteBuilder.BuildRoute(routeSegments))
                .AddAuthentication(AuthService)
                .AddContent(content);
    }
}