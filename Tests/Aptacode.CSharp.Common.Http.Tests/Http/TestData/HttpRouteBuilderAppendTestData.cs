using System.Collections;
using System.Collections.Generic;
using Aptacode.CSharp.Common.Http.Models;

namespace Aptacode.CSharp.Common.Http.Tests.Http.TestData
{
    public class HttpRouteBuilderAppendTestData : IEnumerable<object[]>
    {
        private static readonly ServerAddress ServerAddress = new ServerAddress(Protocol.http, "aptacode.com", 8080);

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                ServerAddress, new[] {"api", "users"},
                new List<object[]>
                {
                    new object[] {"1", "2"},
                    new object[] {"3"},
                    new object[] {"4", "5", "6"}
                },
                "http://aptacode.com:8080/api/users/1/2/3/4/5/6/"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}