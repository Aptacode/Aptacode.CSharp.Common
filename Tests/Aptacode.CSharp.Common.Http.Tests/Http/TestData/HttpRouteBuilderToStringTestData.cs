using System;
using System.Collections;
using System.Collections.Generic;

namespace Aptacode.CSharp.Common.Http.Tests.Http.TestData
{
    public class HttpRouteBuilderToStringTestData : IEnumerable<object[]>
    {
        private static readonly Uri ServerAddress = new UriBuilder("http", "aptacode.com", 8080).Uri;

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {ServerAddress, new[] {"api", "users"}, "http://aptacode.com:8080/api/users/"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}