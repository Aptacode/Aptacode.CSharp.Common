using System.Linq;
using System.Text;

namespace Aptacode.CSharp.Common.Http
{
    public static class StringExtensions
    {
        private const string RouteSeparator = "/";

        public static string RemoveNonLettersAndDigitsFromStart(this string input)
        {
            var firstChar = input[0];
            while (!char.IsLetterOrDigit(firstChar))
            {
                input = input.Remove(0, 1);
                firstChar = input[0];
            }

            return input;
        }

        public static string RemoveNonLettersAndDigitsFromEnd(this string input)
        {
            var lastChar = input[input.Length - 1];
            while (!char.IsLetterOrDigit(lastChar))
            {
                input = input.Remove(input.Length - 1, 1);
                lastChar = input[input.Length - 1];
            }

            return input;
        }

        public static string JoinRoute(this string baseSegment, params object[] routeSegments)
        {
            var builder = new StringBuilder();

            builder.Append(baseSegment);

            if (!baseSegment.EndsWith("/"))
            {
                builder.Append(RouteSeparator);
            }

            foreach (var segment in routeSegments
                .Select(s => s
                    .ToString()
                    .RemoveNonLettersAndDigitsFromEnd()
                    .RemoveNonLettersAndDigitsFromStart()))
            {
                builder.Append(segment);
                builder.Append(RouteSeparator);
            }

            return builder.ToString();
        }
    }

    public class HttpRouteBuilder
    {
        #region Constructor

        public HttpRouteBuilder(ServerAddress serverAddress, params object[] baseRouteSegments)
        {
            ServerAddress = serverAddress;
            ApiBaseRoute = serverAddress.ToString();
            Append(baseRouteSegments);
        }

        #endregion

        public HttpRouteBuilder Append(params object[] baseRouteSegments)
        {
            ApiBaseRoute = ApiBaseRoute.JoinRoute(baseRouteSegments);
            return this;
        }

        public string BuildRoute(params object[] routeSegments) => ApiBaseRoute.JoinRoute(routeSegments);

        public override string ToString() => ApiBaseRoute;

        #region Properties

        public string ApiBaseRoute { get; protected set; }
        public ServerAddress ServerAddress { get; }

        #endregion
    }
}