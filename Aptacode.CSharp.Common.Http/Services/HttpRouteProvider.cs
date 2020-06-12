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

        #endregion

        public string Build(params object[] routeSegments) => ApiBaseRoute.JoinRoute(routeSegments);

        public HttpRouteProvider Append(params object[] baseRouteSegments)
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