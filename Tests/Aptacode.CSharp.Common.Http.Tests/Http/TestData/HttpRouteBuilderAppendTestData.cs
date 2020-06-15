using System;
using System.Collections;
using System.Collections.Generic;

namespace Aptacode.CSharp.Common.Http.Tests.Http.TestData
{
    public class HttpRouteBuilderAppendTestData : IEnumerable<object[]>
    {
        private static readonly Uri ServerAddress = new UriBuilder("http", "aptacode.com", 8080).Uri;

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                ServerAddress, new[] {"api", "users"},
                new List<string[]>
                {
                    new[] {"1", "2"},
                    new[] {"3"},
                    new[] {"4", "5", "6"}
                },
                "http://aptacode.com:8080/api/users/1/2/3/4/5/6/"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}