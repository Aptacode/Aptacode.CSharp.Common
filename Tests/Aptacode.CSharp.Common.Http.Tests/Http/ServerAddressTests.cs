using Aptacode.CSharp.Common.Http.Models;
using Aptacode.CSharp.Common.Http.Tests.Http.TestData;
using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http
{
    public class ServerAddressTests
    {
        [Theory]
        [ClassData(typeof(ServerAddressToStringTestData))]
        public void ToStringTests(Protocol? protocol, string address, int? port, string expectedAddress)
        {
            var sut = new ServerAddress(protocol, address, port);
            Assert.Equal(expectedAddress, sut.ToString());
        }
    }
}