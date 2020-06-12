using System.Collections;
using System.Collections.Generic;
using Aptacode.CSharp.Common.Http.Models;

namespace Aptacode.CSharp.Common.Http.Tests.Http.TestData
{
    public class HttpRouteBuilderBuildRouteTestData : IEnumerable<object[]>
    {
        private static readonly ServerAddress ServerAddress = new ServerAddress(Protocol.http, "aptacode.com", 8080);

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                ServerAddress, new[] {"api", "users"}, new[] {"all", "age"},
                "http://aptacode.com:8080/api/users/all/age/"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}