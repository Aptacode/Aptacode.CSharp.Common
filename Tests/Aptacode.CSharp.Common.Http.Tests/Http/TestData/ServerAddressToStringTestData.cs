using System.Collections;
using System.Collections.Generic;
using Aptacode.CSharp.Common.Http.Models;

namespace Aptacode.CSharp.Common.Http.Tests.Http.TestData
{
    public class ServerAddressToStringTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {Protocol.http, "aptacode.com", 80, "http://aptacode.com:80/"},
            new object[] {Protocol.ftp, "https://aptacode.com", 80, "ftp://aptacode.com:80/"},
            new object[] {Protocol.http, "aptacode.com/", 8080, "http://aptacode.com:8080/"},
            new object[] {null, "aptacode.com", 80, "aptacode.com:80/"},
            new object[] {null, "https://aptacode.com", 80, "aptacode.com:80/"},
            new object[] {null, "aptacode.com", null, "aptacode.com/"},
            new object[] {null, "aptacode.com/", null, "aptacode.com/"},
            new object[] {Protocol.http, "aptacode.com", null, "http://aptacode.com/"},
            new object[] {Protocol.http, "aptacode.com", null, "http://aptacode.com/"}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}