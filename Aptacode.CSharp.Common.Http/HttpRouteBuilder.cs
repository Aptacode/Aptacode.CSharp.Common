namespace Aptacode.CSharp.Common.Http
{
    public class HttpRouteBuilder
    {
        private const string RouteSeparator = "/";

        #region Constructor

        public HttpRouteBuilder(ServerAddress serverAddress, params object[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute =
                $"{serverAddress}{string.Join(RouteSeparator, baseRouteSegments)}{(baseRouteSegments.Length > 0 ? RouteSeparator : string.Empty)}";
        }

        #endregion


        public string BuildRoute(params object[] routeSegments) =>
            $"{ApiBaseRoute}{string.Join(RouteSeparator, routeSegments)}{(routeSegments.Length > 0 ? RouteSeparator : string.Empty)}";

        public override string ToString() => ApiBaseRoute;

        #region Properties

        public string ApiBaseRoute { get; }
        public ServerAddress ServerAddress { get; }

        #endregion
    }
}