using System.Linq;
using System.Text;

namespace Aptacode.CSharp.Common.Http.Services.Extensions
{
    public static class HttpRouteStringExtensions
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

        public static string JoinRoute(this string baseSegment, params string[] routeSegments)
        {
            var builder = new StringBuilder();

            builder.Append(baseSegment);

            if (!baseSegment.EndsWith("/"))
            {
                builder.Append(RouteSeparator);
            }

            if (routeSegments != null)
            {
                foreach (var segment in routeSegments
                    .Select(s => s
                        .ToString()
                        .RemoveNonLettersAndDigitsFromEnd()
                        .RemoveNonLettersAndDigitsFromStart()))
                {
                    builder.Append(segment);
                    builder.Append(RouteSeparator);
                }
            }

            return builder.ToString();
        }


    }
}