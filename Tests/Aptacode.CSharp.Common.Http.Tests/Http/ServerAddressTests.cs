using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http
{
    public class ServerAddressTests
    {
        [Fact]
        public void FullAddressTest()
        {
            var sut = new ServerAddress(){ Address = "aptacode.com", Port = "80", Protocol = "https"};
            Assert.Equal("https://aptacode.com:80/", sut.ToString());
        }

        [Fact]
        public void AddressOnly()
        {
            var sut = new ServerAddress() { Address = "aptacode.com" };
            Assert.Equal("aptacode.com/", sut.ToString());
        }

        [Fact]
        public void ProtocolAndAddress()
        {
            var sut = new ServerAddress() { Address = "aptacode.com", Protocol = "http" };
            Assert.Equal("http://aptacode.com/", sut.ToString());
        }

        [Fact]
        public void AddressAndPort()
        {
            var sut = new ServerAddress() { Address = "aptacode.com", Port = "8080" };
            Assert.Equal("aptacode.com:8080/", sut.ToString());
        }

        [Fact]
        public void NoAddressReturnsEmpty()
        {
            var sut = new ServerAddress() { Protocol = "https", Port = "8080" };
            Assert.Equal(string.Empty, sut.ToString());
        }
    }
}