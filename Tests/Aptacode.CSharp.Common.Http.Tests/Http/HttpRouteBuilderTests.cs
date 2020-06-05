using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http
{
    public class HttpRouteBuilderTests
    {
        private readonly ServerAddress _testAddress = new ServerAddress()
            {Protocol = "https", Address = "aptacode.com", Port = "80"};

        [Fact]
        public void BaseRouteToStringTest()
        {
            var sut = new HttpRouteBuilder(_testAddress, "api","test");
            Assert.Equal("https://aptacode.com:80/api/test/", sut.ToString());
        }

        [Fact]
        public void BuildRouteTest()
        {
            var sut = new HttpRouteBuilder(_testAddress, "api", "test");
            Assert.Equal("https://aptacode.com:80/api/test/users/names", sut.BuildRoute("users","names"));
        }

        [Fact]
        public void EmptyBuildRouteTest()
        {
            var sut = new HttpRouteBuilder(_testAddress, "api", "test");
            Assert.Equal("https://aptacode.com:80/api/test/", sut.BuildRoute());
        }
    }
}