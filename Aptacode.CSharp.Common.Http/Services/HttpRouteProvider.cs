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

        public HttpRouteProvider(ServerAddress serverAddress, params string[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            Join(baseRouteSegments);
        }

        public HttpRouteProvider(ServerAddress serverAddress)
        {
            ServerAddress = serverAddress;
        }

        #endregion

        #region Properties

        public string ApiRoute { get; protected set; } = "";
        public ServerAddress ServerAddress { get; }

        #endregion

        #region Output

        public string Get(params string[] segments) => ToString().JoinRoute(segments);

        public string Get() => ToString();
        public override string ToString() => ServerAddress.ToString().JoinRoute(ApiRoute);

        #endregion

        #region Modify

        public IRouteProvider Append(params string[] segments)
        {
            var newRoute = new HttpRouteProvider(ServerAddress.Copy(), ApiRoute);

            newRoute.Join(segments);

            return newRoute;
        }

        public void Join(params string[] segments)
        {
            if (segments != null)
            {
                ApiRoute = ApiRoute.JoinRoute(segments);
            }
        }

        #endregion
    }
}