using System.Collections.Generic;
using Aptacode.CSharp.Common.Http.Models;
using Aptacode.CSharp.Common.Http.Services;
using Aptacode.CSharp.Common.Http.Tests.Http.TestData;
using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http
{
    public class HttpRouteBuilderTests
    {
        [Theory]
        [ClassData(typeof(HttpRouteBuilderBuildRouteTestData))]
        public void BuildRouteTests(ServerAddress serverAddress, string[] baseComponents, string[] extraComponents,
            string expectedAddress)
        {
            var sut = new HttpRouteProvider(serverAddress, baseComponents);
            Assert.Equal(expectedAddress, sut.Get(extraComponents));
        }

        [Theory]
        [ClassData(typeof(HttpRouteBuilderToStringTestData))]
        public void ToStringTests(ServerAddress serverAddress, string[] baseComponents, string expectedAddress)
        {
            var sut = new HttpRouteProvider(serverAddress, baseComponents);
            Assert.Equal(expectedAddress, sut.ToString());
        }

        [Theory]
        [ClassData(typeof(HttpRouteBuilderAppendTestData))]
        public void AppendTests(ServerAddress serverAddress, string[] baseComponents, List<string[]> components,
            string expectedAddress)
        {
            var sut = new HttpRouteProvider(serverAddress, baseComponents);

            foreach (var component in components)
            {
                sut.Join(component);
            }

            Assert.Equal(expectedAddress, sut.ToString());
        }
    }
}