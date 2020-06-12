using System.Linq;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Http.Models;
using Aptacode.CSharp.Common.Http.Services.Extensions;

namespace Aptacode.CSharp.Common.Http.Services
{
    /// <summary>
    ///     Builds the route for a HttpService
    /// </summary>
    public class HttpRouteProvider : IRouteProvider
    {
        #region Constructor

        public HttpRouteProvider(ServerAddress serverAddress, params object[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute = serverAddress.ToString();
            Append(baseRouteSegments);
        }

        public HttpRouteProvider(ServerAddress serverAddress, params string[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute = serverAddress.ToString();
            Append(baseRouteSegments);
        }

        public HttpRouteProvider(ServerAddress serverAddress)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute = serverAddress.ToString();
        }


        #endregion

        public string Build(params object[] routeSegments) => this.Build(ToStrings(routeSegments));
        public string Build() => ApiBaseRoute;
        public string Build(params string[] routeSegments) => ApiBaseRoute.JoinRoute(routeSegments);

        private static string[] ToStrings(object[] routeSegments)
        {
            if (routeSegments == null || routeSegments.Length == 0)
                return new string[0];

            return routeSegments.Select(s => s.ToString()).ToArray();
        }

        public HttpRouteProvider Append(params object[] baseRouteSegments) => Append(ToStrings(baseRouteSegments));

        public HttpRouteProvider Append(params string[] baseRouteSegments)
        {
            ApiBaseRoute = ApiBaseRoute.JoinRoute(baseRouteSegments);
            return this;
        }

        public override string ToString() => ApiBaseRoute;

        #region Properties

        public string ApiBaseRoute { get; protected set; }
        public ServerAddress ServerAddress { get; }

        #endregion
    }
}