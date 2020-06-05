namespace Aptacode.CSharp.Common.Http
{
    public class HttpRouteBuilder
    {
        private const string RouteSeparator = "/";

        public HttpRouteBuilder(ServerAddress serverAddress, params object[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute = $"{serverAddress}{string.Join(RouteSeparator, baseRouteSegments)}";
        }

        public string ApiBaseRoute { get; }
        public ServerAddress ServerAddress { get; }

        public string BuildRoute(params object[] routeSegments) =>
            $"{ApiBaseRoute}{string.Join(RouteSeparator, routeSegments)}";
    }
}