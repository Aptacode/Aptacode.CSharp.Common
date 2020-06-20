using System.Net.Http;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Http.Services.Extensions;

namespace Aptacode.CSharp.Common.Http.Services
{
    public class AuthenticatedRequestBuilder : IHttpRequestGenerator
    {
        protected readonly IAccessTokenService AuthService;

        public AuthenticatedRequestBuilder(IAccessTokenService authService)
        {
            AuthService = authService;
        }

        public HttpRequestMessage CreateRequest(HttpMethod method, string route) =>
            new HttpRequestMessage()
                .SetMethod(method)
                .SetRoute(route)
                .AddJwtAuthentication(AuthService);

        public HttpRequestMessage CreateRequest<TContent>(HttpMethod method, string route, TContent content) =>
            new HttpRequestMessage()
                .SetMethod(method)
                .SetRoute(route)
                .AddJwtAuthentication(AuthService)
                .AddContent(content);
    }
}